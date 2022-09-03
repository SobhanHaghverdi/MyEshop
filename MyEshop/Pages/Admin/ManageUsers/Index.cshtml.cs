using MyEshop.Models.User;

namespace MyEshop.Pages.Admin.ManageUsers
{
    public class IndexModel : PageModel
    {
        private readonly MyEshopContext _context;

        public IndexModel(MyEshopContext context)
        {
            _context = context;
        }

        public IList<Users> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
