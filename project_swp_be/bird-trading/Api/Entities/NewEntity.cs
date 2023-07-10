using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class NewEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public Guid UserId { get; set; }
        public Guid ClassifyId { get; set; }
        public string Description { get; set; }
    }

    public class GetNewEntityDetail
    {
        public Guid Id { set; get; }
        public string Title { set; get; } = null!;
        public string? ImageUrl { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UserId { get; set; }
        public Guid ClassifyId { get; set; }
        public GetNewEntityDetailUser User { get; set; } = null!;
        public GetNewEntityDetailClassify Classify { get; set; } = null!;
    }

    public class GetNewEntityDetailUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class GetNewEntityDetailClassify
    {
        public string Name { set; get; } = null!;
    }
}