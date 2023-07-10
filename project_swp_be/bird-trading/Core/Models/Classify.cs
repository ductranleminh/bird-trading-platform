using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class Classify
    {
        public Classify() {
            News = new HashSet<New>();
        }

        [Key]
        public Guid Id { set; get; }
        [StringLength(50)]
        public string Name { set; get; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual ICollection<New>? News { get; set; }
    }
}