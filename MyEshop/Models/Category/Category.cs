namespace MyEshop.Models.Category
{
    public class Category
    {
        public Category()
        {

        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام گروه")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "توضیحات گروه")]
        public string Description { get; set; }

        #region Relations

        public virtual ICollection<CategoryToProduct> CategoryToProducts { get; set; }

        #endregion
    }
}
