using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public int Status { get; set; }
        public decimal Money { get; set; }
        public int PaymentType { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }
        [StringLength(int.MaxValue)]
        public string? Description { get; set; }
        public virtual User User { get; set; } = null!;
    }
}