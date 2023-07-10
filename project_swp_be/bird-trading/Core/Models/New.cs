using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class New
    {
        [Key]
        public Guid Id { set; get; }
        [StringLength(200)]
        public string Title { set; get; } = null!;
        [StringLength(int.MaxValue)]
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UserId { get; set; }
        public Guid ClassifyId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Classify Classify { get; set; } = null!;
    }
}