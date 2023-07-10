using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class PackPrice
    {
        [Key]
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime EffectDate { get; set; }
        public Guid PackId { set; get; }
        public virtual Pack Pack { get; set; } = null!;
    }
}