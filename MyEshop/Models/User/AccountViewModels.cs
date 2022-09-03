namespace MyEshop.Models.User
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {

        }

        [MaxLength(50)]
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string LastName { get; set; }

        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Remote("VerifyEmail", "Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور همخوانی ندارد")]
        [Display(Name = "تکرار کلمه عبور")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        public LoginViewModel()
        {

        }

        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
