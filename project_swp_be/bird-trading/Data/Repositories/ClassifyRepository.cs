using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class ClassifyRepository : IClassifyRepository
    {
        private readonly BirdContext _context;

        public ClassifyRepository(BirdContext context)
        {
            _context = context;
        }

        public void Delete(Classify classify)
        {
            var news = (from n in _context.News
                        where n.ClassifyId == classify.Id
                        select n).ToList();

            if (news.Count != 0)
                throw new SystemException();

            _context.Classifies.Remove(classify);
        }

        public GetClassifyDetail Detail(Guid id)
        {
            

            var classify = (from c in _context.Classifies
                            where c.Id == id
                            select new GetClassifyDetail
                            {
                                Id = c.Id,
                                Name = c.Name,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                            }).FirstOrDefault() ?? throw new Exception("Classify with id: " + id + " is not exist");

            var news = (from n in _context.News
                        where n.ClassifyId == id
                        select new GetClassifyDetailNews
                        {
                            Id = n.Id,
                            Title = n.Title,
                            ImageUrl = n.ImageUrl,
                            CreateDate = n.CreateDate,
                            UpdateDate = n.UpdateDate,
                            UserId = n.UserId,
                        });

            classify.TotalNews = news.Count();
            classify.News = news.ToList();

            return classify;
        }

        public Classify Find(Guid id)
        {
            return _context.Classifies.Find(id) ?? throw new Exception("Classify with id: " + id + " is not exist");
        }

        public object Get(int? pageIndex, int? pageSize)
        {
            var query = (from c in _context.Classifies
                         select new
                         {
                             Id = c.Id,
                             Name = c.Name,
                             CreateDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         }).AsQueryable();

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            return query.OrderByDescending(od => od.CreateDate).ToList();
        }

        public void Insert(Classify classify)
        {
            _context.Classifies.Add(classify);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}