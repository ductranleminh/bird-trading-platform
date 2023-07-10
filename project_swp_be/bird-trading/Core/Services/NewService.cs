using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class NewService : INewService
    {
        private readonly INewRepository _repository;
        private NewValidator _validator = new NewValidator();
        public NewService(INewRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Guid id)
        {
            var news = _repository.Find(id);
            _repository.Delete(news);
            _repository.Save();
        }

        public GetNewEntityDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(Guid? classifyId, Guid? userId, int? pageIndex, int? pageSize)
        {
            return _repository.Get(classifyId, userId, pageIndex, pageSize);
        }

        public string Insert(NewEntity news)
        {
            news.Id = Guid.NewGuid();

            if (_validator.ANewValidator(news) != "Ok")
                return _validator.ANewValidator(news);

            var entity = new New {
                Id = news.Id,
                Title = news.Title,
                ImageUrl = news.ImageUrl,
                Description = news.Description,
                CreateDate = DateTime.UtcNow.AddHours(7),
                UpdateDate = DateTime.UtcNow.AddHours(7),
                UserId = news.UserId,
                ClassifyId = news.ClassifyId,
            };

            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string Update(Guid id, NewEntity news)
        {
            var newsUpdate = _repository.Find(id);

            if (_validator.ANewValidator(news) != "Ok")
                return _validator.ANewValidator(news);

            newsUpdate.Title = news.Title;
            newsUpdate.ImageUrl = news.ImageUrl;
            newsUpdate.Description = news.Description;
            newsUpdate.UpdateDate = DateTime.UtcNow.AddHours(7);
            news.UserId = news.UserId;
            news.ClassifyId = news.ClassifyId;

            _repository.Save();

            return "Update Success";
        }
    }
}