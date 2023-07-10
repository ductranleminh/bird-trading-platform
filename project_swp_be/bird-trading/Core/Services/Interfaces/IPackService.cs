using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface IPackService
    {
        object Get(int? pageIndex, int? pageSize);
        PackEntityDetail Detail(Guid id);
        string Insert(PackEntity pack);
        string Update(Guid id, PackEntity pack);
        void Delete(Guid id);
    }
}