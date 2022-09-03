using MyEshop.Data.Repositories;
using MyEshop.Models.Order;
using MyEshop.Models.Product;

namespace MyEshop.Data.Services
{
    public class OrderRepository : IOrderRepository
    {
        private MyEshopContext _context;
        public OrderRepository(MyEshopContext context)
        {
            _context = context;
        }
        public OrderDetail GetOrderDetail(int detailId)
        {
            var detail = _context.OrderDetails.Single(od => od.DetailId == detailId);
            return detail;
        }

        public void AddOrderDetailCount(int detailId)
        {
            var detail = GetOrderDetail(detailId);
            detail.Count += 1;
            Save();
        }


        public void RemoveOrderDetailCount(int detailId)
        {
            var detail = GetOrderDetail(detailId);
            detail.Count -= 1;
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void AddToOrder(int itemId, int userId)
        {
            var product = GetProductDataForOrder(itemId);
            if (product != null)
            {
                var order = GetOrderByUserId(userId);
                if (order != null)
                {
                    var orderDetail = GetOrderDetail(order.OrderId, product.Id);
                    if (orderDetail != null)
                    {
                        AddOrderDetailCount(orderDetail);
                    }
                    else
                    {
                        AddOrderDetail(order, product);
                    }
                }
                else
                {
                    order = new Order()
                    {
                        UserId = userId,
                        CreateDate = DateTime.Now,
                        IsFinally = false
                    };
                    _context.Orders.Add(order);
                    Save();
                    AddOrderDetail(order, product);
                }
                Save();
            }
        }

        public Product GetProductDataForOrder(int itemId)
        {
            return _context.Products.Include(i => i.Item).SingleOrDefault(s => s.ItemId == itemId);
        }

        public Order GetOrderByUserId(int userId)
        {
            return _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinally);
        }

        public void AddOrderDetail(Order order, Product product)
        {
            _context.OrderDetails.Add(new OrderDetail()
            {
                OrderId = order.OrderId,
                ProductId = product.Id,
                Price = product.Item.Price,
                Order = order,
                Count = 1
            });
        }

        public OrderDetail GetOrderDetail(int orderId, int productId)
        {
            return _context.OrderDetails.FirstOrDefault(d => d.OrderId == orderId && d.ProductId == productId);
        }

        public void AddOrderDetailCount(OrderDetail orderDetail)
        {
            orderDetail.Count += 1;
        }

        public Order GetOrderForShowInCart(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId && !o.IsFinally)
                .Include(d => d.OrderDetails)
                .ThenInclude(p => p.Product).FirstOrDefault();
        }

        public OrderDetail GetOrderDetailForRemoveCart(int detailId)
        {
            return _context.OrderDetails.Include(o => o.Order).Single(o => o.DetailId == detailId);
        }

        public void RemoveOrderDetail(OrderDetail orderDetail)
        {
            _context.Remove(orderDetail);
            Save();
        }

        public void RemoveOrder(int detailId)
        {
            var orderDetail = GetOrderDetailForRemoveCart(detailId);
            RemoveOrderDetail(orderDetail);
            OrderDetailExists(orderDetail);
            Save();
        }

        public void OrderDetailExists(OrderDetail orderDetail)
        {
            bool orderDetailExist = _context.OrderDetails.Any(o => o.OrderId == orderDetail.Order.OrderId);

            if (!orderDetailExist)
            {
                _context.Orders.Remove(orderDetail.Order);
            }
        }
    }
}
