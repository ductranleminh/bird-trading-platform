using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly BirdContext _context;

        public MediaRepository(BirdContext context)
        {
            _context = context;
        }
        public void Delete(Media media)
        {
            _context.Medias.Remove(media);
        }

        public MediaEntityDetail Detail(Guid id)
        {
            var media = (from m in _context.Medias
                         join p in _context.Posts on m.PostId equals p.Id
                         where m.Id == id
                         select new MediaEntityDetail
                         {
                             Id = m.Id,
                             Url = m.Url,
                             PostId = m.PostId,
                             Extension = m.Extension,
                             Post = new MediaEntityDetailPost
                             {
                                 Id = p.Id,
                                 CreateDate = p.CreateDate,
                                 Title = p.Title,
                                 Description = p.Description,
                                 Price = p.Price,
                                 Address = p.Address,
                                 Status = p.Status,
                             },
                         }).FirstOrDefault() ?? throw new Exception("Media with id: " + id + " is not exist");
            
            return media;
        }

        public Media Find(Guid id)
        {
            return _context.Medias.Find(id) ?? throw new Exception("Media with id: " + id + " is not exist");
        }

        public object Get(Guid? postId, int? pageIndex, int? pageSize)
        {
            var query = (from m in _context.Medias
                         select new
                         {
                             Id = m.Id,
                             Url = m.Url,
                             PostId = m.PostId,
                             Extension = m.Extension,
                         }).AsQueryable();

            if (postId != null)
                query = query.Where(x => x.PostId == postId);

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            return query.ToList();
        }

        public void Insert(Media media)
        {
            _context.Medias.Add(media);
        }

        public void InsertRange(List<Media> medias)
        {
            _context.Medias.AddRange(medias);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}