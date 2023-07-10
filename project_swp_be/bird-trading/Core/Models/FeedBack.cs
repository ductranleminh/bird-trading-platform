using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class FeedBack
    {
        [Key]
        public Guid Id { set; get; }
        [StringLength(200)]
        public string Title { set; get; } = null!;
        [StringLength(int.MaxValue)]
        public string? Description { get; set; }
        public Guid PostId { set; get; }
        public Guid UserId { set; get; }
        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}