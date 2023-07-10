using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface IUserService
    {
        object Get(int? status, Guid? roleId, int? pageIndex, int? pageSize);
        GetUserEntityDetail Detail(Guid id);
        string Insert(InsertUserEntity user);
        string Update(Guid id, UpdateUserEntity user);
        decimal UpdateBalance(UpdateBalanceUserEntity entity);
        void Delete(Guid id);
    }
}