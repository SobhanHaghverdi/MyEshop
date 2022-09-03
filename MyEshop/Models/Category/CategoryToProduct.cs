namespace MyEshop.Models.Category
{
    public class CategoryToProduct
    {
        public CategoryToProduct()
        {

        }

        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        #region Relations

        public virtual Category Category { get; set; }
        public virtual Product.Product Product { get; set; }

        #endregion

    }
}
