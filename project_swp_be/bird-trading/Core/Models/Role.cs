using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bird_trading.Core.Models
{
    public class Role
	{
        public Role() {
            Users = new HashSet<User>();
        }

        [Key]
        public Guid Id { set; get; }

        [StringLength(20)]
        public string Name { set; get; } = null!;
        [JsonIgnore]
        public virtual ICollection<User>? Users { get; set; }

    }
}

