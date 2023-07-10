using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class Pack
    {
        public Pack() {
            PackPrices = new HashSet<PackPrice>();
            PostTransactions = new HashSet<PostTransaction>();
        }

        [Key]
        public Guid Id { set; get; }
        public int Queue { get; set; }
        [StringLength(200)]
        public string Title { set; get; } = null!;
        public int ExpiredDay { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual ICollection<PackPrice>? PackPrices { get; set; }
        public virtual ICollection<PostTransaction>? PostTransactions { get; set; }
    }
}