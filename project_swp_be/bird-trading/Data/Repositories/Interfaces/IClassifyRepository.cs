using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface IClassifyRepository
    {
        object Get(int? pageIndex, int? pageSize);
        GetClassifyDetail Detail(Guid id);
        Classify Find(Guid id);
        void Insert(Classify classify);
        void Delete(Classify classify);
        void Save();
    }
}