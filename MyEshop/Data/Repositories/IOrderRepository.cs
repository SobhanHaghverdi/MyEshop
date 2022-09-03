namespace MyEshop.Data.Repositories
{
    public interface IOrderRepository
    {
        #region Order

        void AddToOrder(int itemId, int userId);
        void RemoveOrder(int detailId);
        Product GetProductDataForOrder(int itemId);
        Order GetOrderByUserId(int userId);
        Order GetOrderForShowInCart(int userId);

        #endregion
        #region Order Detail

        void AddOrderDetail(Order order, Product product);
        void RemoveOrderDetail(OrderDetail orderDetail);
        OrderDetail GetOrderDetail(int detailId);
        OrderDetail GetOrderDetail(int orderId, int productId);
        OrderDetail GetOrderDetailForRemoveCart(int detailId);
        void AddOrderDetailCount(int detailId);
        void AddOrderDetailCount(OrderDetail orderDetail);
        void RemoveOrderDetailCount(int detailId);
        void OrderDetailExists(OrderDetail orderDetail);

        #endregion
        void Save();
    }
}
