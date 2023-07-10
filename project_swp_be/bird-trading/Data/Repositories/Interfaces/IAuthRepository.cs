using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        string? Login(LoginEntity login);
        string ChangePassword(AuthEntityChangePassword entity);
        void Register(User user);
        bool Exist(string username);
        void Save();
        Guid GetRoleUser();
    }
}