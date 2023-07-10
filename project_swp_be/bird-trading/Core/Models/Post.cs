using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class Post
    {
        public Post()
        {
            FeedBacks = new HashSet<FeedBack>();
            Medias = new HashSet<Media>();
            PostTransactions = new HashSet<PostTransaction>();
        }

        [Key]
        public Guid Id { set; get; }
        public DateTime CreateDate { get; set; }
        [StringLength(200)]
        public string Title { set; get; } = null!;
        [StringLength(int.MaxValue)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        [StringLength(int.MaxValue)]
        public string? Address { get; set; }
        [StringLength(32)]
        public string PhoneSeller { get; set; } = null!;
        [StringLength(100)]
        public string NameSeller { get; set; } = null!;
        public Guid CategoryId { set; get; }
        public Guid UserId { set; get; }
        public int Status { set; get; }
        public virtual ICollection<FeedBack>? FeedBacks { get; set; }
        public virtual ICollection<Media>? Medias { get; set; }
        public virtual ICollection<PostTransaction>? PostTransactions { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}