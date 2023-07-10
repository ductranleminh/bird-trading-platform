using System.ComponentModel.DataAnnotations;

namespace bird_trading.Core.Models
{
    public class Category
    {
        public Category() {
            Posts = new HashSet<Post>();
        }

        [Key]
        public Guid Id { set; get; }
        [StringLength(200)]
        public string Title { set; get; } = null!;
        public int ClassifyCategory { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
    }
}