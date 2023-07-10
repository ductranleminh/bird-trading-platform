using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BirdContext _context;

        public PostRepository(BirdContext context)
        {
            _context = context;
        }
        public void Delete(Post post)
        {
            var feedbacks = (from fb in _context.FeedBacks
                             where fb.PostId == post.Id
                             select fb).ToList();
            if (feedbacks.Count != 0)
                throw new Exception("Constraint with feedback");

            var medias = (from m in _context.Medias
                          where m.PostId == post.Id
                          select m).ToList();
            if (medias.Count != 0)
                throw new Exception("Constraint with media");

            var postTransactions = (from pt in _context.PostTransactions
                                    where pt.PostId == post.Id
                                    select pt).ToList();
            if (postTransactions.Count != 0)
                throw new Exception("Constraint with post transaction");

            _context.Posts.Remove(post);
        }

        public PostEntityDetail Detail(Guid id)
        {
            var post = (from p in _context.Posts
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join u in _context.Users on p.UserId equals u.Id
                        where p.Id == id
                        select new PostEntityDetail
                        {
                            Id = p.Id,
                            CreateDate = p.CreateDate,
                            Title = p.Title,
                            Description = p.Description,
                            Price = p.Price,
                            Address = p.Address,
                            PhoneSeller = p.PhoneSeller,
                            NameSeller = p.NameSeller,
                            CategoryId = p.CategoryId,
                            UserId = p.UserId,
                            Status = p.Status,
                            Category = new PostEntityDetailCategory
                            {
                                Id = c.Id,
                                Title = c.Title,
                                ClassifyCategory = c.ClassifyCategory,
                            },
                            User = new PostEntityDetailUser
                            {
                                Id = u.Id,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Username = u.Username,
                                PhoneNumber = u.PhoneNumber,
                            },
                        }).FirstOrDefault() ?? throw new Exception("Pack with id: " + id + " is not exist");

            var feedbacks = (from fb in _context.FeedBacks
                             where fb.PostId == id
                             select new PostEntityDetailFeedback
                             {
                                 Id = fb.Id,
                                 Title = fb.Title,
                                 Description = fb.Description,
                                 PostId = fb.Id,
                             }).ToList();
            post.FeedBacks = feedbacks;

            var medias = (from m in _context.Medias
                          where m.PostId == post.Id
                          select new PostEntityDetailMedia
                          {
                              Id = m.Id,
                              Url = m.Url,
                              PostId = m.PostId,
                              Extension = m.Extension,
                          }).ToList();
            post.Medias = medias;

            var postTransactions = (from pt in _context.PostTransactions
                                    join p in _context.Packs on pt.PackId equals p.Id
                                    where pt.PostId == post.Id
                                    select new PostEntityDetailPostTransaction
                                    {
                                        Id = pt.Id,
                                        Price = pt.Price,
                                        CreateDate = pt.CreateDate,
                                        EffectDate = pt.EffectDate,
                                        ExpiredDay = pt.ExpiredDay,
                                        IsCancel = pt.IsCancel,
                                        PackId = pt.PackId,
                                        PostId = pt.PostId,
                                        Queue = p.Queue,
                                    }).OrderByDescending(o => o.CreateDate).ToList();
            post.PostTransactions = postTransactions;

            return post;
        }

        public Post Find(Guid id)
        {
            return _context.Posts.Find(id) ?? throw new Exception("Post with id: " + id + " is not exist");
        }

        public object Get(Guid? categoryId, Guid? packId, Guid? userId, int? status, int? pageIndex, int? pageSize)
        {
            var query = (from p in _context.Posts
                         join c in _context.Categories on p.CategoryId equals c.Id
                         select new
                         {
                             Id = p.Id,
                             CreateDate = p.CreateDate,
                             Title = p.Title,
                             Description = p.Description,
                             Price = p.Price,
                             Address = p.Address,
                             PhoneSeller = p.PhoneSeller,
                             NameSeller = p.NameSeller,
                             UserId = p.UserId,
                             Status = p.Status,
                             CategoryId = c.Id,
                             CategoryTitle = c.Title,
                             Media = (from m in _context.Medias
                                      where m.PostId == p.Id
                                      select new PostEntityDetailMedia
                                      {
                                          Id = m.Id,
                                          Url = m.Url,
                                          PostId = m.PostId,
                                          Extension = m.Extension,
                                      }).ToList(),
                             PostTransaction = (from pt in _context.PostTransactions
                                                join pack in _context.Packs on pt.PackId equals pack.Id
                                                where pt.IsCancel == false && pt.EffectDate.AddDays(pt.ExpiredDay) > DateTime.UtcNow.AddHours(7) && pt.PostId == p.Id
                                                select new
                                                {
                                                    Id = pt.Id,
                                                    price = pt.Price,
                                                    CreateDate = pt.CreateDate,
                                                    EffectDate = pt.EffectDate,
                                                    ExpiredDay = pt.ExpiredDay,
                                                    PackId = pack.Id,
                                                    Queue = pack.Queue,
                                                    IsCancel = pt.IsCancel,
                                                }).OrderByDescending(od => od.CreateDate).FirstOrDefault(),
                         }).AsQueryable();

            if (categoryId != null)
                query = query.Where(w => w.CategoryId == categoryId);

            if (userId != null)
                query = query.Where(w => w.UserId == userId);

            if (status != null)
            {
                if (status == -1)
                {
                    var query1 = (from p in _context.Posts
                                  join c in _context.Categories on p.CategoryId equals c.Id
                                  select new
                                  {
                                      Id = p.Id,
                                      CreateDate = p.CreateDate,
                                      Title = p.Title,
                                      Description = p.Description,
                                      Price = p.Price,
                                      Address = p.Address,
                                      PhoneSeller = p.PhoneSeller,
                                      NameSeller = p.NameSeller,
                                      UserId = p.UserId,
                                      Status = p.Status,
                                      CategoryId = c.Id,
                                      CategoryTitle = c.Title,
                                      Media = (from m in _context.Medias
                                               where m.PostId == p.Id
                                               select new PostEntityDetailMedia
                                               {
                                                   Id = m.Id,
                                                   Url = m.Url,
                                                   PostId = m.PostId,
                                                   Extension = m.Extension,
                                               }).ToList(),
                                      PostTransaction = (from pt in _context.PostTransactions
                                                         join pack in _context.Packs on pt.PackId equals pack.Id
                                                         where pt.PostId == p.Id
                                                         select new
                                                         {
                                                             Id = pt.Id,
                                                             price = pt.Price,
                                                             CreateDate = pt.CreateDate,
                                                             EffectDate = pt.EffectDate,
                                                             ExpiredDay = pt.ExpiredDay,
                                                             PackId = pack.Id,
                                                             Queue = pack.Queue,
                                                             IsCancel = pt.IsCancel,
                                                         }).OrderByDescending(od => od.CreateDate).FirstOrDefault(),
                                  }).AsQueryable();

                    query1 = query1.Where(w => w.Status == status);
                    query1 = query1.Where(w => w.PostTransaction != null && w.PostTransaction.Queue > 1);
                    query1 = query1.OrderBy(od => od.PostTransaction.Queue).ThenByDescending(q => q.CreateDate);

                    return query1.ToList();
                }
                else
                    query = query.Where(w => w.Status == status);
            }

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            query = query.Where(w => w.PostTransaction != null && w.PostTransaction.Queue > 1 && w.PostTransaction.EffectDate <= DateTime.UtcNow.AddHours(7));
            query = query.OrderBy(od => od.PostTransaction.Queue).ThenByDescending(q => q.CreateDate);

            return query.ToList();
        }

        public object GetPostBanner(Guid? categoryId, Guid? userId, int? status, int? pageIndex, int? pageSize)
        {
            var query = (from p in _context.Posts
                         join c in _context.Categories on p.CategoryId equals c.Id
                         select new
                         {
                             Id = p.Id,
                             CreateDate = p.CreateDate,
                             Title = p.Title,
                             Description = p.Description,
                             Price = p.Price,
                             Address = p.Address,
                             PhoneSeller = p.PhoneSeller,
                             NameSeller = p.NameSeller,
                             UserId = p.UserId,
                             Status = p.Status,
                             CategoryId = c.Id,
                             CategoryTitle = c.Title,
                             Media = (from m in _context.Medias
                                      where m.PostId == p.Id
                                      select new PostEntityDetailMedia
                                      {
                                          Id = m.Id,
                                          Url = m.Url,
                                          PostId = m.PostId,
                                          Extension = m.Extension,
                                      }).ToList(),
                             PostTransaction = (from pt in _context.PostTransactions
                                                join pack in _context.Packs on pt.PackId equals pack.Id
                                                where pt.IsCancel == false && pt.EffectDate.AddDays(pt.ExpiredDay) > DateTime.UtcNow.AddHours(7) && pt.PostId == p.Id && pack.Queue == 1
                                                select new
                                                {
                                                    Id = pt.Id,
                                                    price = pt.Price,
                                                    CreateDate = pt.CreateDate,
                                                    EffectDate = pt.EffectDate,
                                                    ExpiredDay = pt.ExpiredDay,
                                                    PackId = pack.Id,
                                                    Queue = pack.Queue,
                                                    IsCancel = pt.IsCancel,
                                                }).OrderByDescending(od => od.CreateDate).FirstOrDefault(),
                         }).AsQueryable();

            if (categoryId != null)
                query = query.Where(w => w.CategoryId == categoryId);

            if (userId != null)
                query = query.Where(w => w.UserId == userId);

            if (status != null)
            {
                if (status == -1)
                {
                    var query1 = (from p in _context.Posts
                                  join c in _context.Categories on p.CategoryId equals c.Id
                                  select new
                                  {
                                      Id = p.Id,
                                      CreateDate = p.CreateDate,
                                      Title = p.Title,
                                      Description = p.Description,
                                      Price = p.Price,
                                      Address = p.Address,
                                      PhoneSeller = p.PhoneSeller,
                                      NameSeller = p.NameSeller,
                                      UserId = p.UserId,
                                      Status = p.Status,
                                      CategoryId = c.Id,
                                      CategoryTitle = c.Title,
                                      Media = (from m in _context.Medias
                                               where m.PostId == p.Id
                                               select new PostEntityDetailMedia
                                               {
                                                   Id = m.Id,
                                                   Url = m.Url,
                                                   PostId = m.PostId,
                                                   Extension = m.Extension,
                                               }).ToList(),
                                      PostTransaction = (from pt in _context.PostTransactions
                                                         join pack in _context.Packs on pt.PackId equals pack.Id
                                                         where pt.PostId == p.Id && pack.Queue == 1
                                                         select new
                                                         {
                                                             Id = pt.Id,
                                                             price = pt.Price,
                                                             CreateDate = pt.CreateDate,
                                                             EffectDate = pt.EffectDate,
                                                             ExpiredDay = pt.ExpiredDay,
                                                             PackId = pack.Id,
                                                             Queue = pack.Queue,
                                                             IsCancel = pt.IsCancel,
                                                         }).OrderByDescending(od => od.CreateDate).FirstOrDefault(),
                                  }).AsQueryable();

                    query1 = query1.Where(w => w.Status == status);
                    query = query.Where(w => w.PostTransaction != null && w.PostTransaction.Queue == 1);
                    query1 = query1.OrderBy(od => od.PostTransaction.Queue).ThenByDescending(q => q.CreateDate);

                    return query1.OrderByDescending(od => od.CreateDate).ToList();
                }
                else
                    query = query.Where(w => w.Status == status);
            }

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            query = query.Where(w => w.PostTransaction != null && w.PostTransaction.Queue == 1);

            return query.OrderByDescending(od => od.CreateDate).ToList();
        }

        public object GetPostProcess(Guid? categoryId, Guid? packId, Guid? userId, int? pageIndex, int? pageSize)
        {
            var query = (from p in _context.Posts
                         join c in _context.Categories on p.CategoryId equals c.Id
                         where p.Status == 0
                         select new
                         {
                             Id = p.Id,
                             CreateDate = p.CreateDate,
                             Title = p.Title,
                             Description = p.Description,
                             Price = p.Price,
                             Address = p.Address,
                             PhoneSeller = p.PhoneSeller,
                             NameSeller = p.NameSeller,
                             UserId = p.UserId,
                             Status = p.Status,
                             CategoryId = c.Id,
                             CategoryTitle = c.Title,
                             Media = (from m in _context.Medias
                                      where m.PostId == p.Id
                                      select new PostEntityDetailMedia
                                      {
                                          Id = m.Id,
                                          Url = m.Url,
                                          PostId = m.PostId,
                                          Extension = m.Extension,
                                      }).ToList(),
                             PostTransaction = (from pt in _context.PostTransactions
                                                join pack in _context.Packs on pt.PackId equals pack.Id
                                                where pt.IsCancel == true && pt.EffectDate.Date >= DateTime.UtcNow.AddHours(7).Date && pt.PostId == p.Id
                                                select new
                                                {
                                                    Id = pt.Id,
                                                    CreateDate = pt.CreateDate,
                                                    PackId = pack.Id,
                                                    Queue = pack.Queue,
                                                    IsCancel = pt.IsCancel,
                                                }).OrderByDescending(od => od.CreateDate).FirstOrDefault(),
                         }).AsQueryable();
            var query1 = _context.Posts.Where(a => a.Status == 0).ToList();
            foreach (var item in query1)
            {
                var qr = _context.PostTransactions.Where(a => a.PostId.Equals(item.Id)).OrderByDescending(a=>a.CreateDate).FirstOrDefault();
                if(qr!.EffectDate.Date < DateTime.UtcNow.AddHours(7).Date)
                {
                    item.Status = -1;
                }
            }
            _context.SaveChanges();

            if (categoryId != null)
                query = query.Where(w => w.CategoryId == categoryId);

            if (userId != null)
                query = query.Where(w => w.UserId == userId);

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            //query = query.Where(w => w.PostTransaction != null && w.PostTransaction.Queue > 1);
            query = query.Where(w => w.PostTransaction != null);
            query = query.OrderBy(od => od.PostTransaction.Queue).ThenBy(q => q.CreateDate);

            return query.ToList();
        }

        public void Insert(Post post)
        {
            _context.Posts.Add(post);
        }

        public string ProcessPost(PostEntityProcess entityProcess)
        {
            var post = (from p in _context.Posts
                        join pt in _context.PostTransactions on p.Id equals pt.PostId
                        where p.Id == entityProcess.postId
                        select p).FirstOrDefault() ?? throw new Exception("Post with id: " + entityProcess.postId + " is not exist");

            if (post.Status != 0)
                return "This post is approved or denided";


            if (entityProcess.type == 0)
            {
                post.Status = -1;
                return "Denide successful";
            }

            else if (entityProcess.type == 1)
            {
                var user = (from u in _context.Users
                            where u.Id == post.UserId
                            select u).FirstOrDefault() ?? throw new Exception("User with id: " + post.UserId + " is not exist");

                var postTransaction = (from pt in _context.PostTransactions
                                       where pt.PostId == entityProcess.postId
                                       select pt).FirstOrDefault() ?? throw new Exception("Post Transaction with PostId: " + entityProcess.postId + " is not exist");

                if (postTransaction.IsCancel != true)
                    return "This post is approved or denided";

                if (user.Balance < postTransaction.Price)
                    throw new Exception("User is not enough balance to approve");

                user.Balance = user.Balance - postTransaction.Price;
                postTransaction.IsCancel = false;
                post.Status = 1;
                return "Approve successful";
            }

            else
                return "Invalid argument type";
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}