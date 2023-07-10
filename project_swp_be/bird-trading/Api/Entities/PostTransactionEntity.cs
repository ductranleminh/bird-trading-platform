using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class PostTransactionEntity
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime EffectDate { get; set; }
        public int ExpiredDay { get; set; }
        public Guid PackId { set; get; }
        public Guid PostId { set; get; }
    }

    public class PostTransactionEntityDetail
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EffectDate { get; set; }
        public int ExpiredDay { get; set; }
        public bool IsCancel { get; set; }
        public Guid PackId { set; get; }
        public Guid PostId { set; get; }
        public PostTransactionEntityDetailPack Pack { get; set; } = null!;
        public PostTransactionEntityDetailPost Post { get; set; } = null!;
    }

    public class PostTransactionEntityDetailPack
    {
        public Guid Id { set; get; }
        public int Queue { get; set; }
        public string Title { set; get; } = null!;
        public int ExpiredDay { get; set; }
    }

    public class PostTransactionEntityDetailPost
    {
        public Guid Id { set; get; }
        public DateTime CreateDate { get; set; }
        public string Title { set; get; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public int Status { set; get; }
    }

    public class PostTransactionEntityProcess
    {
        public Guid postTransactionId { get; set; }
        public int type { get; set; }
    }

    public class PostTransactionEntityIsCancel
    {
        public Guid postTransactionId { get; set; }
        public bool IsCancel { get; set; }
    }
}