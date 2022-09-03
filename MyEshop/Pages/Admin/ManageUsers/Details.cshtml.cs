using MyEshop.Models.User;

namespace MyEshop.Pages.Admin.ManageUsers
{
    public class DetailsModel : PageModel
    {
        private readonly MyEshopContext _context;

        public DetailsModel(MyEshopContext context)
        {
            _context = context;
        }

        public Users Users { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Users = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);

            if (Users == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
