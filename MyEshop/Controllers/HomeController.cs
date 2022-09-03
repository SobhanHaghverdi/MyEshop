using System.Diagnostics;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        private MyEshopContext _context;

        public HomeController(MyEshopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(i => i.Item).ToList();
            return View(products);
        }
        #region Info

        [Route("AboutUs")]
        public IActionResult AboutUs()
        {
            return View();
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("404NotFound")]
        public IActionResult NotFoundError()
        {
            return View();
        }
    }
}
