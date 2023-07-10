using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class User
    {
        public User() {
            FeedBacks = new HashSet<FeedBack>();
            News = new HashSet<New>();
            Posts = new HashSet<Post>();
            // Replies = new HashSet<Reply>();
            // Topics = new HashSet<Topic>();
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [StringLength(int.MaxValue)]
        public string? Address { get; set; }
        [StringLength(100)]
        public string Username { get; set; } = null!;
        [StringLength(int.MaxValue)]
        public string Password { get; set; } = null!;
        [StringLength(320)]
        public string? Email { get; set; }
        [StringLength(32)]
        public string? PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<BankInfomation>? BankInfomations { get; set; }
        public virtual ICollection<FeedBack>? FeedBacks { get; set; }
        public virtual ICollection<New>? News { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        // public virtual ICollection<Reply>? Replies { get; set; }
        // public virtual ICollection<Topic>? Topics { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}