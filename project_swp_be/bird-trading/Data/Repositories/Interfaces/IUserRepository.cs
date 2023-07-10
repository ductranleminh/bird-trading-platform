using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        object Get(int? status, Guid? roleId, int? pageIndex, int? pageSize);
        GetUserEntityDetail Detail(Guid id);
        bool Exist(string username);
        User Find(Guid id);
        void Insert(User user);
        void Delete(User user);
        void Save();
    }
}