namespace MyEshop.Models.User
{
    public class Users
    {
        public Users()
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
        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "مدیر")]
        public bool IsAdmin { get; set; }

        #region Relations

        public List<Order.Order> Orders { get; set; }

        #endregion

    }
}
