namespace bird_trading.Infrastructure.Extensions.Security
{
    public class HashPassword
    {
        public string EncryptPass(string password) {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool DecryptPass(string input, string result) {
            return BCrypt.Net.BCrypt.Verify(input, result);
        }
    }
}