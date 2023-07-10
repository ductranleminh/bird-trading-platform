using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface INewRepository
    {
        object Get(Guid? classifyId, Guid? userId, int? pageIndex, int? pageSize);
        GetNewEntityDetail Detail(Guid id);
        New Find(Guid id);
        void Insert(New news);
        void Delete(New news);
        void Save();
    }
}