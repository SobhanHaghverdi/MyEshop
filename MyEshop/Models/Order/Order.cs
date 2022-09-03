namespace MyEshop.Models.Order
{
    public class Order
    {
        public Order()
        {

        }

        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsFinally { get; set; }

        #region Relations

        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

        #endregion

    }
}
