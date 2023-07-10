using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class BankInfomation
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [StringLength(200)]
        public string Name { set; get; } = null!;
        public int AccountNumber { set; get; }
        public virtual User User { get; set; } = null!;
    }
}