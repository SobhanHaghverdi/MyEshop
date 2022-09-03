namespace MyEshop.Models.User
{
    public class ProfileInformationViewModel
    {
        public ProfileInformationViewModel()
        {

        }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
    }
    public class EditProfileViewModel
    {
        public EditProfileViewModel()
        {

        }

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [Display(Name = "کلمه عبور")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }
    }
}
