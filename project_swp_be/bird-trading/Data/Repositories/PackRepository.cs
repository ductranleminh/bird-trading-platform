using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class PackRepository : IPackRepository
    {
        private readonly BirdContext _context;

        public PackRepository(BirdContext context)
        {
            _context = context;
        }

        public void Delete(Pack pack)
        {
            var packPrices = (from pp in _context.PackPrices
                              where pp.PackId == pack.Id
                              select pp).ToList();
            if (packPrices.Count != 0)
                throw new Exception("Constraint with packprice");

            var postTransactions = (from pt in _context.PostTransactions
                                    where pt.PackId == pack.Id
                                    select pt).ToList();
            if (postTransactions.Count != 0)
                throw new Exception("Constraint with post transactions");

            _context.Packs.Remove(pack);
        }

        public PackEntityDetail Detail(Guid id)
        {
            var pack = (from p in _context.Packs
                        where p.Id == id
                        select new PackEntityDetail
                        {
                            Id = p.Id,
                            Queue = p.Queue,
                            Title = p.Title,
                            ExpiredDay = p.ExpiredDay,
                            CreateDate = p.CreateDate,
                            UpdateDate = p.UpdateDate,
                        }).FirstOrDefault() ?? throw new Exception("Pack with id: " + id + " is not exist");

            var packPrice = (from pp in _context.PackPrices
                             where pp.PackId == pack.Id
                             select new PackEntityDetailPackPrice
                             {
                                 Id = pp.Id,
                                 Price = pp.Price,
                                 EffectDate = pp.EffectDate,
                                 PackId = pp.PackId,
                             });
            pack.TotalPackPrice = packPrice.Count();
            pack.packPrices = packPrice.OrderByDescending(x => x.EffectDate).ToList();

            return pack;
        }

        public Pack Find(Guid id)
        {
            return _context.Packs.Find(id) ?? throw new Exception("Pack with id: " + id + " is not exist");
        }

        public object Get(int? pageIndex, int? pageSize)
        {
            var query = (from p in _context.Packs
                         select new
                         {
                             Id = p.Id,
                             Queue = p.Queue,
                             Title = p.Title,
                             ExpiredDay = p.ExpiredDay,
                             CreateDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                             Price = (from pp in _context.PackPrices
                                      where pp.PackId == p.Id && DateTime.UtcNow.AddHours(7) >= pp.EffectDate
                                      select new PackEntityDetailPackPrice {
                                        Id = pp.Id, Price = pp.Price, EffectDate = pp.EffectDate, PackId = pp.PackId,
                                      }).OrderByDescending(x => x.EffectDate).FirstOrDefault(),
                         }).AsQueryable();

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            return query.OrderByDescending(od => od.Queue).ToList();
        }

        public void Insert(Pack pack)
        {
            _context.Packs.Add(pack);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}