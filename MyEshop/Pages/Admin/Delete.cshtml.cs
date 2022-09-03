using MyEshop.Models.Product;

namespace MyEshop.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private MyEshopContext _context;
        public DeleteModel(MyEshopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _context.Products.FirstOrDefault(p => p.Id == id);
        }
        public IActionResult OnPost()
        {
            var product = _context.Products.Find(Product.Id);
            var item = _context.Items.First(i => i.Id == product.ItemId);

            _context.Products.Remove(product);
            _context.Items.Remove(item);

            _context.SaveChanges();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    product.Id + ".jpg");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return RedirectToPage("Index");
        }
    }
}
