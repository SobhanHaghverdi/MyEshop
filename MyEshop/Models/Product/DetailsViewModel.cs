namespace MyEshop.Models.Product
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {

        }

        public Product Product { get; set; }
        public List<Category.Category> Categories { get; set; }
    }
}
