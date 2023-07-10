using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class NewRepository : INewRepository
    {
        private readonly BirdContext _context;

        public NewRepository(BirdContext context)
        {
            _context = context;
        }

        public void Delete(New news)
        {
            _context.News.Remove(news);
        }

        public GetNewEntityDetail Detail(Guid id)
        {
            var news = (from n in _context.News
                        join c in _context.Classifies on n.ClassifyId equals c.Id
                        join u in _context.Users on n.UserId equals u.Id
                        where n.Id == id
                        select new GetNewEntityDetail {
                            Id = n.Id,
                            Title = n.Title,
                            ImageUrl = n.ImageUrl,
                            Url = n.Url,
                            Description = n.Description,
                            CreateDate = n.CreateDate,
                            UpdateDate = n.UpdateDate,
                            UserId = n.UserId,
                            ClassifyId = n.ClassifyId,
                            User = new GetNewEntityDetailUser {
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Username = u.Username,
                                Email = u.Email,
                                PhoneNumber = u.PhoneNumber,
                            },
                            Classify = new GetNewEntityDetailClassify {
                                Name = c.Name,
                            }
                        }).FirstOrDefault() ?? throw new Exception("News with id: " + id + " is not exist");

            return news;
        }

        public New Find(Guid id)
        {
            return _context.News.Find(id) ?? throw new Exception("News with id: " + id + " is not exist"); 
        }

        public object Get(Guid? classifyId, Guid? userId, int? pageIndex, int? pageSize)
        {
            var query = (from n in _context.News
                        select new {
                            Id = n.Id,
                            Title = n.Title,
                            ImageUrl = n.ImageUrl,
                            Url = n.Url,
                            Description = n.Description,
                            CreateDate = n.CreateDate,
                            UpdateDate = n.UpdateDate,
                            UserId = n.UserId,
                            ClassifyId = n.ClassifyId,
                        }).AsQueryable();

            if (classifyId != null)
                query = query.Where(x => x.ClassifyId == classifyId);

            if (userId != null)
                query = query.Where(x => x.UserId == userId);

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            return query.OrderByDescending(od => od.CreateDate).ToList();
        }

        public void Insert(New news)
        {
            _context.News.Add(news);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}