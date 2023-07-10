using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface IPackPriceService
    {
        object Get(int? pageIndex, int? pageSize);
        PackPriceEntityDetail Detail(Guid id);
        string Insert(PackPriceEntity packPrice);
        string Update(Guid id, PackPriceEntity packPrice);
        void Delete(Guid id);
    }
}