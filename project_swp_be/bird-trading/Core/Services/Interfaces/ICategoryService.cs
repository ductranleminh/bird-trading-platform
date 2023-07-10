using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface ICategoryService
    {
        object Get(int? pageIndex, int? pageSize);
        GetCategoryDetail Detail(Guid id);
        string Insert(CategoryEntity category);
        string Update(Guid id, CategoryEntity category);
        void Delete(Guid id);
    }
}