using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface IClassifyService
    {
        object Get(int? pageIndex, int? pageSize);
        GetClassifyDetail Detail(Guid id);
        string Insert(ClassifyEntity classify);
        string Update(Guid id, ClassifyEntity classify);
        void Delete(Guid id);
    }
}