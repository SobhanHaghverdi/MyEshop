namespace MyEshop.Models.Order
{
    public class OrderDetail
    {
        public OrderDetail()
        {

        }

        [Key]
        public int DetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Count { get; set; }

        #region Relations

        [ForeignKey("ProductId")]
        public virtual Product.Product Product { get; set; }
        public virtual Order Order { get; set; }

        #endregion

    }
}
