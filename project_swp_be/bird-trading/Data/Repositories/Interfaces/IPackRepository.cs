using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface IPackRepository
    {
        object Get(int? pageIndex, int? pageSize);
        PackEntityDetail Detail(Guid id);
        Pack Find(Guid id);
        void Insert(Pack pack);
        void Delete(Pack pack);
        void Save();
    }
}