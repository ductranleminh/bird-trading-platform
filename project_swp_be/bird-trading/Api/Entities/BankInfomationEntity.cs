using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class BankInfomationEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { set; get; } = null!;
        public int AccountNumber { set; get; }
    }

    public class BankInfomationEntityDetail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { set; get; } = null!;
        public int AccountNumber { set; get; }
        public BankInfomationEntityDetailUser User { get; set; } = null!;
    }

    public class BankInfomationEntityDetailUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}