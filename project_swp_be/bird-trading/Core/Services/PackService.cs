using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class PackService : IPackService
    {
        private readonly IPackRepository _repository;
        private PackValidator _validator = new PackValidator();
        public PackService(IPackRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Guid id)
        {
            var pack = _repository.Find(id);
            _repository.Delete(pack);
            _repository.Save();
        }

        public PackEntityDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(int? pageIndex, int? pageSize)
        {
            return _repository.Get(pageIndex, pageSize);
        }

        public string Insert(PackEntity pack)
        {
            pack.Id = Guid.NewGuid();

            if (_validator.APackValidator(pack) != "Ok")
                return _validator.APackValidator(pack);

            var entity = new Pack
            {
                Id = pack.Id,
                Queue = pack.Queue,
                Title = pack.Title,
                ExpiredDay = pack.ExpiredDay,
                CreateDate = DateTime.UtcNow.AddHours(7),
                UpdateDate = DateTime.UtcNow.AddHours(7),
            };

            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string Update(Guid id, PackEntity pack)
        {
            var packUpdate = _repository.Find(id);

            if (_validator.APackValidator(pack) != "Ok")
                return _validator.APackValidator(pack);

            packUpdate.Queue = pack.Queue;
            packUpdate.Title = pack.Title;
            packUpdate.ExpiredDay = pack.ExpiredDay;
            packUpdate.UpdateDate = DateTime.UtcNow.AddHours(7);

            _repository.Save();

            return "Update Success";
        }
    }
}