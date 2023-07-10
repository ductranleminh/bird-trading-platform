using System.Text.Json.Serialization;

namespace bird_trading.Api.Entities
{
    public class UserEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class GetUserEntityDetail
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string Username { get; set; } = null!;
        public Guid RoleId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
        public decimal Balance { get; set; }
        public int TotalBank { get; set; }
        public int TotalPosts { get; set; }
        //public int TotalTopics { get; set; }
        public int TotalTransactions { get; set; }
        public IList<GetUserEntityDetailBankInfomation>? BankInfomations { get; set; }
        public IList<GetUserEntityDetailPost>? Posts { get; set; }
        // public IList<GetUserEntityDetailTopic>? Topics { get; set; }
        public IList<GetUserEntityDetailTransaction>? Transactions { get; set; }
        public GetUserEntityDetailRole? Role { get; set; }
    }

    public class GetUserEntityDetailBankInfomation
    {
        
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { set; get; } = null!;
        public int AccountNumber { set; get; }
    }

    public class GetUserEntityDetailPost
    {
        public Guid Id { set; get; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public Guid CategoryId { set; get; }
        public int Status { get; set; }
        public int TotalMedias { get; set; }
        public int TotalPostTransactions { get; set; }
        public IList<GetUserEntityDetailMedia>? Medias { get; set; }
        public IList<GetUserEntityDetailPostTransaction>? PostTransactions { get; set; }
    }

    public class GetUserEntityDetailMedia
    {
        public Guid Id { set; get; }
        public string? Url { get; set; }
        public Guid PostId { set; get; }
        public string Extension { set; get; } = null!;
    }

    public class GetUserEntityDetailPostTransaction
    {
        public Guid Id { set; get; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EffectDate { get; set; }
        public int ExpiredDay { get; set; }
        public DateTime ValidDate { get; set; }
        public bool IsCancel { get; set; }
        public Guid PackId { set; get; }
        public Guid PostId { set; get; }
        public GetUserEntityDetailPack? Packs { get; set; }
    }

    public class GetUserEntityDetailPack
    {
        public Guid Id { set; get; }
        public int Queue { get; set; }
        public string Title { set; get; } = null!;
        public int ExpiredDay { get; set; }
    }

    // public class GetUserEntityDetailTopic
    // {
    //     public Guid Id { set; get; }
    //     public DateTime CreateDate { get; set; }
    //     public DateTime UpdateDate { get; set; }
    //     public string Title { get; set; } = null!;
    //     public string? Description { get; set; }
    //     public int Status { get; set; }
    // }

    public class GetUserEntityDetailTransaction
    {
        public Guid Id { set; get; }
        public int Status { get; set; }
        public decimal Money { get; set; }
        public int PaymentType { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { set; get; }
        public string? Description { get; set; }
    }

    public class GetUserEntityDetailRole
    {
        public string Name { set; get; } = null!;
    }

    public class InsertUserEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid RoleId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Balance { get; set; }
    }

    public class UpdateUserEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class UpdateBalanceUserEntity
    {
        public Guid Id { get; set; }
        public decimal money { get; set; }
    }
}