using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class ClassifyEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class GetClassifyDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int TotalNews { get; set; }
        public virtual IList<GetClassifyDetailNews>? News { get; set; }
    }

    public class GetClassifyDetailNews
    {
        public Guid Id { set; get; }
        public string Title { set; get; } = null!;
        public string? ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UserId { get; set; }
    }
}