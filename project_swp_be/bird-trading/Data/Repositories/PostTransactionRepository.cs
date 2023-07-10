using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class PostTransactionRepository : IPostTransactionRepository
    {
        private readonly BirdContext _context;

        public PostTransactionRepository(BirdContext context)
        {
            _context = context;
        }
        public void Delete(PostTransaction postTransaction)
        {
            _context.PostTransactions.Remove(postTransaction);
        }

        public PostTransactionEntityDetail Detail(Guid id)
        {
            var postTransaction = (from pt in _context.PostTransactions
                                   join pack in _context.Packs on pt.PackId equals pack.Id
                                   join post in _context.Posts on pt.PostId equals post.Id
                                   where pt.Id == id
                                   select new PostTransactionEntityDetail
                                   {
                                       Id = pt.Id,
                                       Price = pt.Price,
                                       CreateDate = pt.CreateDate,
                                       EffectDate = pt.EffectDate,
                                       ExpiredDay = pt.ExpiredDay,
                                       IsCancel = pt.IsCancel,
                                       PackId = pt.PackId,
                                       PostId = pt.PostId,
                                       Pack = new PostTransactionEntityDetailPack
                                       {
                                           Id = pack.Id,
                                           Queue = pack.Queue,
                                           Title = pack.Title,
                                           ExpiredDay = pack.ExpiredDay,
                                       },
                                       Post = new PostTransactionEntityDetailPost
                                       {
                                           Id = post.Id,
                                           CreateDate = post.CreateDate,
                                           Title = post.Title,
                                           Description = post.Description,
                                           Price = post.Price,
                                           Address = post.Address,
                                           Status = post.Status,
                                       },
                                   }).FirstOrDefault() ?? throw new Exception("Post Transaction with id: " + id + " is not exist");

            return postTransaction;
        }

        public PostTransaction Find(Guid id)
        {
            return _context.PostTransactions.Find(id) ?? throw new Exception("Post Transaction with id: " + id + " is not exist");
        }

        public object Get(Guid? postId, Guid? packId, bool? IsCancel, int? pageIndex, int? pageSize)
        {
            var query = (from pt in _context.PostTransactions
                         select new
                         {
                             Id = pt.Id,
                             Price = pt.Price,
                             CreateDate = pt.CreateDate,
                             EffectDate = pt.EffectDate,
                             ExpiredDay = pt.ExpiredDay,
                             IsCancel = pt.IsCancel,
                             PackId = pt.PackId,
                             PostId = pt.PostId,
                         }).AsQueryable();

            if (postId != null)
                query = query.Where(x => x.PostId == postId);

            if (packId != null)
                query = query.Where(x => x.PackId == packId);

            if (IsCancel != null)
                query = query.Where(x => x.IsCancel == IsCancel);

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            return query.OrderByDescending(od => od.CreateDate).ToList();
        }

        public void Insert(PostTransaction postTransaction)
        {
            // if (postTransaction.EffectDate < DateTime.UtcNow.AddHours(7))
            //     throw new Exception("EffectDate must be > " + DateTime.UtcNow.AddHours(7));

            var query = (from pt in _context.PostTransactions
                         where pt.PostId == postTransaction.PostId && pt.EffectDate.AddDays(pt.ExpiredDay) > DateTime.UtcNow.AddHours(7) && pt.IsCancel == false
                         select new
                         {
                             Id = pt.Id,
                             Price = pt.Price,
                             CreateDate = pt.CreateDate,
                             EffectDate = pt.EffectDate,
                             ExpiredDay = pt.ExpiredDay,
                             IsCancel = pt.IsCancel,
                             PackId = pt.PackId,
                             PostId = pt.PostId,
                         }).ToList();

            foreach (var x in query)
            {
                if (postTransaction.EffectDate >= x.EffectDate && postTransaction.EffectDate <= x.EffectDate.AddDays(postTransaction.ExpiredDay))
                    throw new Exception("Date Effect is duplicate");
                if (postTransaction.EffectDate.AddDays(postTransaction.ExpiredDay) >= x.EffectDate && postTransaction.EffectDate.AddDays(postTransaction.ExpiredDay) <= x.EffectDate.AddDays(postTransaction.ExpiredDay))
                    throw new Exception("Date Effect is duplicate");
            }

            _context.PostTransactions.Add(postTransaction);
        }

        public string ProcessPostTransaction(PostTransactionEntityProcess entityProcess)
        {
            var postTransaction = (from pt in _context.PostTransactions
                                   where pt.Id == entityProcess.postTransactionId
                                   select pt).FirstOrDefault() ?? throw new Exception("Post Transaction with id: " + entityProcess.postTransactionId + " is not exist");

            if (postTransaction.IsCancel != true)
                return "This post is approved or denided";

            if (entityProcess.type == 0)
            {
                _context.PostTransactions.Remove(postTransaction);
                return "Denide successful";
            }

            else if (entityProcess.type == 1)
            {
                var post = (from p in _context.Posts
                            where p.Id == postTransaction.PostId
                            select p).FirstOrDefault() ?? throw new Exception("Post with id: " + postTransaction.PostId + " is not exist");

                var user = (from u in _context.Users
                            where u.Id == post.UserId
                            select u).FirstOrDefault() ?? throw new Exception("User with id: " + post.UserId + " is not exist");

                if (user.Balance < postTransaction.Price)
                    throw new Exception("User is not enough balance to approve");

                postTransaction.IsCancel = false;
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