using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private CategoryValidator _validator = new CategoryValidator();
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public void Delete(Guid id)
        {
            var category = _repository.Find(id);
            _repository.Delete(category);
            _repository.Save();
        }

        public GetCategoryDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(int? pageIndex, int? pageSize)
        {
            return _repository.Get(pageIndex, pageSize);
        }

        public string Insert(CategoryEntity category)
        {
            category.Id = Guid.NewGuid();

            if (_validator.ACategoryValidator(category) != "Ok")
                return _validator.ACategoryValidator(category);

            var entity = new Category
            {
                Id = category.Id,
                Title = category.Title,
                ClassifyCategory = category.ClassifyCategory,
                CreateDate = DateTime.UtcNow.AddHours(7),
                UpdateDate = DateTime.UtcNow.AddHours(7),
            };

            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string Update(Guid id, CategoryEntity category)
        {
            var categoryUpdate = _repository.Find(id);

            if (_validator.ACategoryValidator(category) != "Ok")
                return _validator.ACategoryValidator(category);

            categoryUpdate.Title = category.Title;
            categoryUpdate.ClassifyCategory = category.ClassifyCategory;
            categoryUpdate.UpdateDate = DateTime.UtcNow.AddHours(7);

            _repository.Save();

            return "Update Success";
        }
    }
}