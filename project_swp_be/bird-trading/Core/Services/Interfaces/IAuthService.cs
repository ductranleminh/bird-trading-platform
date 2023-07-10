using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface IAuthService
	{
        string? Login(LoginEntity login);
        string ChangePassword(AuthEntityChangePassword entity);
        string Resgister(UserEntity user);
    }
}

