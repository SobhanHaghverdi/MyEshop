using MyEshop.Data.Repositories;
using MyEshop.Models.User;

namespace MyEshop.Data.Services
{
    public class UserRepository : IUserRepository
    {
        private MyEshopContext _context;
        public UserRepository(MyEshopContext context)
        {
            _context = context;
        }

        public Users GetUserByUserId(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.UserId == userId);
        }

        public Users GetUserForLogin(string email, string password)
        {
            return _context.Users
                .SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        public ProfileInformationViewModel GetUserInformation(int userId)
        {
            var user = GetUserByUserId(userId);
            ProfileInformationViewModel profileViewModel = new ProfileInformationViewModel();
            profileViewModel.FirstName = user.FirstName;
            profileViewModel.LastName = user.LastName;
            profileViewModel.Email = user.Email;
            profileViewModel.RegisterDate = user.RegisterDate;

            return profileViewModel;
        }

        public bool IsExistUserByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public EditProfileViewModel GetDataForEditUserProfile(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId).Select(u => new EditProfileViewModel()
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Password = u.Password
            }).Single();
        }

        public void EditProfile(int userId, EditProfileViewModel editProfileViewModel)
        {
            var user = GetUserByUserId(userId);
            user.FirstName = editProfileViewModel.FirstName;
            user.LastName = editProfileViewModel.LastName;
            user.Password = editProfileViewModel.Password;

            UpdateUser(user);
        }

        public void UpdateUser(Users user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public void AddUser(RegisterViewModel register)
        {
            Users user = new Users()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email.ToLower(),
                Password = register.Password,
                IsAdmin = false,
                RegisterDate = DateTime.Now
            };
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
