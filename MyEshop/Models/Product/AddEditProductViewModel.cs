namespace MyEshop.Models.Product
{
    public class AddEditProductViewModel
    {
        public AddEditProductViewModel()
        {

        }

        public int Id { get; set; }

        [Display(Name = "نام محصول")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "قیمت")]
        public decimal Price { get; set; }

        [Display(Name = "موجود در انبار")]
        public int QuantityInStock { get; set; }

        [Display(Name = "تصویر :")]
        public IFormFile Picture { get; set; }
        public List<Category.Category> Categories { get; set; }

    }
}
