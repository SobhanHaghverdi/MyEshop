namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ProfileInformation");
            }
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            _userRepository.AddUser(register);

            return View("SuccessRegister", register);
        }
        public IActionResult VerifyEmail(string email)
        {
            if (_userRepository.IsExistUserByEmail(email.ToLower()))
            {
                return Json($"ایمیل {email} قبلا ثبت شده است");
            }
            return Json(true);
        }
        #endregion
        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ProfileInformation");
            }
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userRepository.GetUserForLogin(login.Email.ToLower(), login.Password);

            if (user == null)
            {
                ModelState.AddModelError("Email", "اطلاعات وارد شده صحیح نمی باشد");
                return View(login);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.FirstName+" "+user.LastName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("IsAdmin",user.IsAdmin.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = login.RememberMe
            };

            HttpContext.SignInAsync(principal, properties);

            return Redirect("/");
        }
        #endregion
        #region Logout

        [Route("Logout")]
        public IActionResult Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion
        #region User Panel

        [Route("Profile")]
        [Authorize]
        public IActionResult ProfileInformation()
        {
            return View(_userRepository.GetUserInformation(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }

        [Route("EditProfile")]
        [Authorize]
        public IActionResult UpdateProfileInformation()
        {
            return View(_userRepository.GetDataForEditUserProfile(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }

        [Route("EditProfile")]
        [HttpPost]
        public IActionResult UpdateProfileInformation(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _userRepository.EditProfile(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), model);

            Logout();

            return RedirectToAction("Login");
        }

        #endregion
    }
}
