using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class MediaEntity
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public string? Url { get; set; }
        public Guid PostId { set; get; }
        public string Extension { set; get; } = null!;
    }

    public class MediaEntityDetail
    {
        public Guid Id { set; get; }
        public string? Url { get; set; }
        public Guid PostId { set; get; }
        public string Extension { set; get; } = null!;
        public MediaEntityDetailPost Post { get; set; } = null!;
    }

    public class MediaEntityDetailPost
    {
        public Guid Id { set; get; }
        public DateTime CreateDate { get; set; }
        public string Title { set; get; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public int Status { set; get; }
    }
}