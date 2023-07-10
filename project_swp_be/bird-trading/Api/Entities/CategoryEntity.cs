using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class CategoryEntity
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public string Title { set; get; } = null!;
        public int ClassifyCategory { get; set; }
    }

    public class GetCategoryDetail
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int ClassifyCategory { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int TotalPosts { get; set; }
        public IList<GetCategoryDetailPost>? Posts { get; set; }
    }

    public class GetCategoryDetailPost
    {
        public Guid Id { set; get; }
        public DateTime CreateDate { get; set; }
        public string Title { set; get; } = null!;
        public string? Description { set; get; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public Guid CategoryId { set; get; }
        public Guid UserId { set; get; }
        public int Status { set; get; }
    }
}