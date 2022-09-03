using MyEshop.Models.User;

namespace MyEshop.Pages.Admin.ManageUsers
{
    public class EditModel : PageModel
    {
        private readonly MyEshopContext _context;

        public EditModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Users).State = EntityState.Modified;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(Users.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Login", "Account");
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
