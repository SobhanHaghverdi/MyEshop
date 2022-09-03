namespace MyEshop.Models.Product
{
    public class Item
    {
        public Item()
        {

        }

        public int Id { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        #region Relations

        public virtual Product Product { get; set; }

        #endregion
    }
}
