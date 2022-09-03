using ZarinpalSandbox;

namespace MyEshop.Controllers
{
    public class ProductController : Controller
    {
        private MyEshopContext _context;
        private IOrderRepository _orderRepository;
        public ProductController(MyEshopContext context, IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        #region Cart

        [Authorize]
        public IActionResult AddToCart(int itemId)
        {
            _orderRepository.AddToOrder(itemId, int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()));
            return RedirectToAction("ShowCart");
        }

        [Authorize]
        [Route("Cart")]
        public IActionResult ShowCart()
        {
            return View(_orderRepository.GetOrderForShowInCart(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString())));
        }

        [Authorize]
        public IActionResult RemoveFromCart(int detailId)
        {
            _orderRepository.RemoveOrder(detailId);
            return RedirectToAction("ShowCart");
        }
        [Authorize]
        public IActionResult AddProductCount(int detailId)
        {
            _orderRepository.AddOrderDetailCount(detailId);
            return RedirectToAction("ShowCart");
        }
        [Authorize]
        public IActionResult RemoveProductCount(int detailId)
        {
            _orderRepository.RemoveOrderDetailCount(detailId);
            return RedirectToAction("ShowCart");
        }

        #endregion
        #region Product
        public IActionResult Detail(int id)
        {
            var product = _context.Products
                .Include(p => p.Item)
                .SingleOrDefault(s => s.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p => p.Id == id)
                .SelectMany(s => s.CategoryToProducts)
                .Select(ca => ca.Category).ToList();

            var vm = new DetailsViewModel()
            {
                Product = product,
                Categories = categories
            };

            return View(vm);
        }

        [Route("Categories/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id, string name)
        {
            ViewData["GroupName"] = name;

            var product = _context.CategoryToProducts
                .Where(c => c.CategoryId == id)
                .Include(c => c.Product)
                .ThenInclude(c => c.Item)
                .Select(c => c.Product)
                .ToList();

            return View(product);
        }
        #endregion
        #region Payment
        [Authorize]
        public IActionResult Payment()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinally);
            if (order == null)
            {
                return NotFound();
            }

            var payment = new Payment((int)order.OrderDetails.Sum(s => s.Price * s.Count));

            var res = payment.PaymentRequest($"خرید فاکتور شماره {order.OrderId}",
                "http://localhost:14244/Product/OnlinePayment/" + order.OrderId, userEmail, "09051051055");

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return View("FailedPayment");
            }
        }
        [Authorize]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var order = _context.Orders.Include(o => o.OrderDetails)
                    .ThenInclude(p => p.Product)
                    .ThenInclude(i => i.Item)
                    .FirstOrDefault(o => o.OrderId == id);

                var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price * d.Count));
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    order.IsFinally = true;
                    _context.Orders.Update(order);
                    _context.SaveChanges();
                    ViewBag.code = res.RefId;
                    return View(order.OrderDetails);
                }
            }

            return View();
        }

        #endregion
    }
}
