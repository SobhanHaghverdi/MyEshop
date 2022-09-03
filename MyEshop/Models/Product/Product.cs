namespace MyEshop.Models.Product
{
    public class Product
    {
        public Product()
        {

        }

        public int Id { get; set; }

        [Display(Name = "نام محصول")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        public int ItemId { get; set; }

        #region Relations

        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }
        public Item Item { get; set; }

        #endregion
    }
}
