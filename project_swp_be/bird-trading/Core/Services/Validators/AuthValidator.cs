using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Validators
{
    public class AuthValidator
    {
        public AuthValidator()
        {
        }
        public string RegisterValidator(UserEntity user) {

            if (user.FirstName.Length > 50)
                return "FirstName max length is 50";

            if (user.LastName.Length > 50)
                return "LastName max length is 50";
            
            if (user.Username.Length > 100)
                return "Username max length is 100";

            if (user.Password.Length > 64)
                return "Password max length is 64";

            if (user.Email is not null && user.Email.Length > 320)
                return "Email max length is 320";

            if (user.PhoneNumber is not null && user.PhoneNumber.Length > 32)
                return "PhoneNumber max length is 32";

            return "Ok";
        }
    }
}