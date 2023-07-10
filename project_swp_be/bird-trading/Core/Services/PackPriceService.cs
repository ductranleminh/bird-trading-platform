using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class PackPriceService : IPackPriceService
    {
        private readonly IPackPriceRepository _repository;
        public PackPriceService(IPackPriceRepository repository)
        {
            _repository = repository;
        }
        public void Delete(Guid id)
        {
            var packPrice = _repository.Find(id);
            _repository.Delete(packPrice);
            _repository.Save();
        }

        public PackPriceEntityDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(int? pageIndex, int? pageSize)
        {
            return _repository.Get(pageIndex, pageSize);
        }

        public string Insert(PackPriceEntity packPrice)
        {
            var listPackPrice = _repository.GetAllByPackId(packPrice.PackId);
            foreach(var x in listPackPrice) {
                if (packPrice.EffectDate == x.EffectDate)
                    throw new DuplicateWaitObjectException("EffectDate is exist");
            }
            packPrice.Id = Guid.NewGuid();

            var entity = new PackPrice
            {
                Id = packPrice.Id,
                Price = packPrice.Price,
                EffectDate = packPrice.EffectDate,
                PackId = packPrice.PackId
            };

            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string Update(Guid id, PackPriceEntity packPrice)
        {
            var packPriceUpdate = _repository.Find(id);

            packPriceUpdate.Price = packPrice.Price;
            packPriceUpdate.EffectDate = packPrice.EffectDate;
            packPriceUpdate.PackId = packPrice.PackId;

            _repository.Save();

            return "Update Success";
        }
    }
}