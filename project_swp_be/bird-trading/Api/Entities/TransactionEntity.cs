using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class TransactionEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Status { get; set; }
        public decimal Money { get; set; }
        public int PaymentType { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }
        public string? Description { get; set; }
    }

    public class TransactionMakeRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public decimal Money { get; set; }
        public int PaymentType { get; set; }
        public Guid UserId { get; set; }
        public string? Description { get; set; }
    }

    public class TransactionEntityDetail
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
        public decimal Money { get; set; }
        public int PaymentType { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }
        public string? Description { get; set; }
        public TransactionEntityDetailUser User { get; set; } = null!;
    }

    public class TransactionEntityDetailUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }

    public class TransactionEntityProcess
    {
        public Guid TransactionId { get; set; }
        public int type { get; set; }
    }
}