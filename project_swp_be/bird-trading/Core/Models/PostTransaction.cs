using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class PostTransaction
    {
        [Key]
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EffectDate { get; set; }
        public int ExpiredDay { get; set; }
        public bool IsCancel { get; set; }
        public Guid PackId { set; get; }
        public Guid PostId { set; get; }
        public virtual Pack Pack { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}