using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class Reply
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UserId { get; set; }
        public bool IsDelete { get; set; }
        public Guid TopicId { get; set; }
        [StringLength(int.MaxValue)]
        public string? Description { get; set; }
        public virtual Topic Topic { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}