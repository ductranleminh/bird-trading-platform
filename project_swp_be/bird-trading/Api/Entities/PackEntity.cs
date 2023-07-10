using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class PackEntity
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public int Queue { get; set; }
        public string Title { set; get; } = null!;
        public int ExpiredDay { get; set; }
    }

    public class PackEntityDetail
    {
        public Guid Id { set; get; }
        public int Queue { get; set; }
        public string Title { set; get; } = null!;
        public int ExpiredDay { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int TotalPackPrice { get; set; }
        public virtual IList<PackEntityDetailPackPrice>? packPrices { get; set; }
    }

    public class PackEntityDetailPackPrice
    {
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime EffectDate { get; set; }
        public Guid PackId { set; get; }
    }
}