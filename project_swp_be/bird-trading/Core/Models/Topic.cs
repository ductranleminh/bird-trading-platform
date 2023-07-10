using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class Topic
    {
        public Topic() {
            Replies = new HashSet<Reply>();
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UserId { get; set; }
        [StringLength(200)]
        public string Title { set; get; } = null!;
        [StringLength(int.MaxValue)]
        public string? Description { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Reply>? Replies { get; set; }
        public virtual User User { get; set; } = null!;
    }
}