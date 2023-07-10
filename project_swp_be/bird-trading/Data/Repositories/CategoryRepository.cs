using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BirdContext _context;

        public CategoryRepository(BirdContext context)
        {
            _context = context;
        }

        public void Delete(Category category)
        {
            var posts = (from p in _context.Posts
                         where p.CategoryId == category.Id
                         select p).ToList();

            if (posts.Count != 0)
                throw new SystemException();

            _context.Categories.Remove(category);
        }

        public GetCategoryDetail Detail(Guid id)
        {
            var category = (from c in _context.Categories
                            where c.Id == id
                            select new GetCategoryDetail
                            {
                                Id = c.Id,
                                Title = c.Title,
                                ClassifyCategory = c.ClassifyCategory,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                            }).FirstOrDefault() ?? throw new Exception("Category with id: " + id + " is not exist");

            var posts = (from p in _context.Posts
                        where p.CategoryId == id
                        select new GetCategoryDetailPost
                        {
                            Id = p.Id,
                            CreateDate = p.CreateDate,
                            Title = p.Title,
                            Description = p.Description,
                            Price = p.Price,
                            Address = p.Address,
                            CategoryId = p.CategoryId,
                            UserId = p.UserId,
                            Status = p.Status,
                        });
            
            category.TotalPosts = posts.Count();
            category.Posts = posts.ToList();

            return category;
        }

        public Category Find(Guid id)
        {
            return _context.Categories.Find(id) ?? throw new Exception("Category with id: " + id + " is not exist");
        }

        public object Get(int? pageIndex, int? pageSize)
        {
            var query = (from c in _context.Categories
                         select new
                         {
                             Id = c.Id,
                             Title = c.Title,
                             ClassifyCategory = c.ClassifyCategory,
                             CreateDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         }).AsQueryable();

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            return query.OrderByDescending(od => od.CreateDate).ToList();
        }

        public void Insert(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}