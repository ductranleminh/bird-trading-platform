namespace bird_trading.Api.Entities
{
    public class AuthEntity
    {

    }

    public class AuthEntityChangePassword
    {
        public string Username { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}