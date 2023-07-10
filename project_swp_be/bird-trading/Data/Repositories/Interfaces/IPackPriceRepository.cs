using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface IPackPriceRepository
    {
        object Get(int? pageIndex, int? pageSize);
        IList<PackPriceEntity> GetAllByPackId(Guid packId);
        PackPriceEntityDetail Detail(Guid id);
        PackPrice Find(Guid id);
        void Insert(PackPrice packPrice);
        void Delete(PackPrice packPrice);
        void Save();
    }
}