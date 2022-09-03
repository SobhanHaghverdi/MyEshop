using MyEshop.Models.Product;

namespace MyEshop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Product> Products { get; set; }
        private MyEshopContext _context;
        public IndexModel(MyEshopContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Products = _context.Products.Include(p => p.Item);
        }
        public void OnPost()
        {
        }
    }
}
