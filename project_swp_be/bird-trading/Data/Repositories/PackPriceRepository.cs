using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class PackPriceRepository : IPackPriceRepository
    {
        private readonly BirdContext _context;

        public PackPriceRepository(BirdContext context)
        {
            _context = context;
        }

        public void Delete(PackPrice packPrice)
        {
            _context.PackPrices.Remove(packPrice);
        }

        public PackPriceEntityDetail Detail(Guid id)
        {
            var packPrice = (from pp in _context.PackPrices
                             where pp.Id == id
                             select new PackPriceEntityDetail
                             {
                                 Id = pp.Id,
                                 Price = pp.Price,
                                 EffectDate = pp.EffectDate,
                                 PackId = pp.PackId,
                             }).FirstOrDefault() ?? throw new Exception("PackPrice with id: " + id + " is not exist");

            var pack = (from p in _context.Packs
                        where p.Id == packPrice.PackId
                        select new PackPriceEntityDetailPack
                        {
                            Id = p.Id,
                            Queue = p.Queue,
                            Title = p.Title,
                            ExpiredDay = p.ExpiredDay,
                            CreateDate = p.CreateDate,
                            UpdateDate = p.UpdateDate,
                        });

            packPrice.Pack = pack.FirstOrDefault();

            return packPrice;
        }

        public PackPrice Find(Guid id)
        {
            return _context.PackPrices.Find(id) ?? throw new Exception("PackPrice with id: " + id + " is not exist");
        }

        public object Get(int? pageIndex, int? pageSize)
        {
            var query = (from pp in _context.PackPrices
                         select new
                         {
                             Id = pp.Id,
                             Price = pp.Price,
                             EffectDate = pp.EffectDate,
                             PackId = pp.PackId,
                         }).AsQueryable();

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            return query.ToList();
        }

        public IList<PackPriceEntity> GetAllByPackId(Guid packId)
        {
            var query = (from pp in _context.PackPrices
                        where pp.PackId == packId
                        select new PackPriceEntity{
                            Id = pp.Id,
                            Price = pp.Price,
                            EffectDate = pp.EffectDate,
                            PackId = pp.PackId
                        }).AsQueryable();
            
            return query.ToList();
        }

        public void Insert(PackPrice packPrice)
        {
            _context.PackPrices.Add(packPrice);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}