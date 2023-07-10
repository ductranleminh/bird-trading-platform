using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class PostEntity
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public string Title { set; get; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public Guid CategoryId { set; get; }
        public Guid UserId { set; get; }
        public int Status { set; get; }
    }

    public class PostEntityInsert
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public string Title { set; get; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public string PhoneSeller { get; set; } = null!;
        public string NameSeller { get; set; } = null!;
        public Guid CategoryId { set; get; }
        public Guid UserId { set; get; }
        public List<PostEntityInsertMedias> medias { get; set; } = null!;
        public PostEntityInsertPostTransaction postTransaction { get; set; } = null!;
    }

    public class PostEntityInsertMedias
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public string? Url { get; set; }
        public string Extension { set; get; } = null!;
    }

    public class PostEntityInsertPostTransaction
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime EffectDate { get; set; }
        public int ExpiredDay { get; set; }
        public Guid PackId { set; get; }
    }

    public class PostEntityDetail
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public DateTime CreateDate { get; set; }
        public string Title { set; get; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public string PhoneSeller { get; set; } = null!;
        public string NameSeller { get; set; } = null!;
        public Guid CategoryId { set; get; }
        public Guid UserId { set; get; }
        public int Status { set; get; }
        public IList<PostEntityDetailFeedback>? FeedBacks { get; set; }
        public IList<PostEntityDetailMedia>? Medias { get; set; }
        public IList<PostEntityDetailPostTransaction>? PostTransactions { get; set; }
        public PostEntityDetailCategory Category { get; set; } = null!;
        public PostEntityDetailUser User { get; set; } = null!;
    }

    public class PostEntityDetailFeedback
    {
        public Guid Id { set; get; }
        public string Title { set; get; } = null!;
        public string? Description { get; set; }
        public Guid PostId { set; get; }
    }

    public class PostEntityDetailMedia
    {
        public Guid Id { set; get; }
        public string? Url { get; set; }
        public Guid PostId { set; get; }
        public string Extension { set; get; } = null!;
    }

    public class PostEntityDetailPostTransaction
    {
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EffectDate { get; set; }
        public int ExpiredDay { get; set; }
        public bool IsCancel { get; set; }
        public Guid PackId { set; get; }
        public Guid PostId { set; get; }
        public int Queue { set; get; }
    }

    public class PostEntityDetailCategory
    {
        public Guid Id { set; get; }
        public string Title { set; get; } = null!;
        public int ClassifyCategory { get; set; }
    }

    public class PostEntityDetailUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }

    public class PostEntityProcess
    {
        public Guid postId { get; set; }
        public int type { get; set; }
    }

    public class PostEntityUpdateStatus
    {
        public Guid postId { get; set; }
        public int status { get; set; }
    }
}