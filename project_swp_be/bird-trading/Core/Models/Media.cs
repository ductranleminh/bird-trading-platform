using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class Media
    {
        [Key]
        public Guid Id { set; get; }
        [StringLength(int.MaxValue)]
        public string? Url { get; set; }
        public Guid PostId { set; get; }
        [StringLength(10)]
        public string Extension { set; get; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}