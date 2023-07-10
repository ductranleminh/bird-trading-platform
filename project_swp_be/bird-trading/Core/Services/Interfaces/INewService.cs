using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface INewService
    {
        object Get(Guid? classifyId, Guid? userId, int? pageIndex, int? pageSize);
        GetNewEntityDetail Detail(Guid id);
        string Insert(NewEntity news);
        string Update(Guid id, NewEntity news);
        void Delete(Guid id);
    }
}