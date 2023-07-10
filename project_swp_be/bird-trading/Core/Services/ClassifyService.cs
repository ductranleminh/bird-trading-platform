using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class ClassifyService : IClassifyService
    {
        private readonly IClassifyRepository _repository;
        private ClassifyValidator _validator = new ClassifyValidator();
        public ClassifyService(IClassifyRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Guid id)
        {
            var classify = _repository.Find(id);
            _repository.Delete(classify);
            _repository.Save();
        }

        public GetClassifyDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(int? pageIndex, int? pageSize)
        {
            return _repository.Get(pageIndex, pageSize);
        }

        public string Insert(ClassifyEntity classify)
        {
            classify.Id = Guid.NewGuid();

            if (_validator.AClassifyValidator(classify) != "Ok")
                return _validator.AClassifyValidator(classify);

            var entity = new Classify {
                Id = classify.Id,
                Name = classify.Name,
                CreateDate = DateTime.UtcNow.AddHours(7),
                UpdateDate = DateTime.UtcNow.AddHours(7),
            };

            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string Update(Guid id, ClassifyEntity classify)
        {
            var classifyUpdate = _repository.Find(id);

            if (_validator.AClassifyValidator(classify) != "Ok")
                return _validator.AClassifyValidator(classify);

            classifyUpdate.Name = classify.Name;
            classifyUpdate.UpdateDate = DateTime.UtcNow.AddHours(7);

            _repository.Save();

            return "Update Success";
        }
    }
}