using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class PackPriceEntity
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime EffectDate { get; set; }
        public Guid PackId { set; get; }
    }

    public class PackPriceEntityDetail
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime EffectDate { get; set; }
        public Guid PackId { set; get; }
        public PackPriceEntityDetailPack? Pack { get; set; }
    }

    public class PackPriceEntityDetailPack
    {
        public Guid Id { set; get; }
        public int Queue { get; set; }
        public string Title { set; get; } = null!;
        public int ExpiredDay { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}