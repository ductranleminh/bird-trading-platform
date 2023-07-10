using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        object Get(int? pageIndex, int? pageSize);
        GetCategoryDetail Detail(Guid id);
        Category Find(Guid id);
        void Insert(Category category);
        void Delete(Category category);
        void Save();
    }
}