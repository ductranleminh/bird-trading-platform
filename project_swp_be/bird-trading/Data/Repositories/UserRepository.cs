using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BirdContext _context;

        public UserRepository(BirdContext context)
        {
            _context = context;
        }

        public void Delete(User user)
        {
            var news = (from n in _context.News
                        where n.UserId == user.Id
                        select n).ToList();

            if (news.Count() != 0)
                throw new Exception("Constraint with news");

            _context.Users.Remove(user);
        }

        public GetUserEntityDetail Detail(Guid id)
        {


            var user = (from u in _context.Users
                        where u.Id == id
                        select new GetUserEntityDetail
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Address = u.Address,
                            Username = u.Username,
                            RoleId = u.RoleId,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            CreateDate = u.CreateDate,
                            UpdateDate = u.UpdateDate,
                            Status = u.Status,
                            Balance = u.Balance,
                        }).FirstOrDefault() ?? throw new Exception("User with id: " + id + " is not exist");

            var medias = (from m in _context.Medias
                          select new GetUserEntityDetailMedia
                          {
                              Id = m.Id,
                              Url = m.Url,
                              PostId = m.PostId,
                              Extension = m.Extension,
                          }).AsQueryable();

            var pack = (from p in _context.Packs
                        select new GetUserEntityDetailPack
                        {
                            Id = p.Id,
                            Queue = p.Queue,
                            Title = p.Title,
                            ExpiredDay = p.ExpiredDay,
                        }).AsQueryable();

            var postTransactions = (from pt in _context.PostTransactions
                                    select new GetUserEntityDetailPostTransaction
                                    {
                                        Id = pt.Id,
                                        Price = pt.Price,
                                        CreateDate = pt.CreateDate,
                                        EffectDate = pt.EffectDate,
                                        ExpiredDay = pt.ExpiredDay,
                                        ValidDate = pt.EffectDate.AddDays(pt.ExpiredDay),
                                        IsCancel = pt.IsCancel,
                                        PackId = pt.PackId,
                                        PostId = pt.PostId,
                                        Packs = pack.Where(x => x.Id == pt.PackId).FirstOrDefault(),
                                    }).AsQueryable();

            var bankInfo = (from bi in _context.BankInfomations
                            where bi.UserId == id
                            select new GetUserEntityDetailBankInfomation
                            {
                                Id = bi.Id,
                                UserId = bi.UserId,
                                Name = bi.Name,
                                AccountNumber = bi.AccountNumber,
                            });

            var posts = (from p in _context.Posts
                         where p.UserId == id
                         select new GetUserEntityDetailPost
                         {
                             Id = p.Id,
                             CreateDate = p.CreateDate,
                             Title = p.Title,
                             Description = p.Description,
                             Price = p.Price,
                             Address = p.Address,
                             CategoryId = p.CategoryId,
                             Status = p.Status,
                             TotalMedias = medias.Where(x => x.PostId == p.Id).Count(),
                             Medias = medias.Where(x => x.PostId == p.Id).ToList(),
                             TotalPostTransactions = postTransactions.Where(x => x.PostId == p.Id).Count(),
                             PostTransactions = postTransactions.Where(x => x.PostId == p.Id).ToList(),
                         });
            var transactions = from ts in _context.Transactions
                               where ts.UserId == id
                               select new GetUserEntityDetailTransaction
                               {
                                   Id = ts.Id,
                                   Status = ts.Status,
                                   Money = ts.Money,
                                   PaymentType = ts.PaymentType,
                                   CreateDate = ts.CreateDate,
                                   UserId = ts.UserId,
                                   Description = ts.Description,
                               };

            var role = from r in _context.Roles
                       where r.Id == user.RoleId
                       select new GetUserEntityDetailRole
                       {
                           Name = r.Name
                       };
            var query1 = _context.Posts.Where(a => a.Status == 0).ToList();
            foreach (var item in query1)
            {
                var qr = _context.PostTransactions.Where(a => a.PostId.Equals(item.Id)).OrderByDescending(a => a.CreateDate).FirstOrDefault();
                if (qr!.EffectDate.Date < DateTime.UtcNow.AddHours(7).Date)
                {
                    item.Status = -1;
                }
            }
            _context.SaveChanges();
            user.TotalBank = bankInfo.Count();
            user.BankInfomations = bankInfo.ToList();
            user.TotalPosts = posts.Count();
            user.Posts = posts.OrderByDescending(u => u.CreateDate).ToList();
            user.TotalTransactions = transactions.Count();
            user.Transactions = transactions.ToList();
            user.Role = role.FirstOrDefault();

            return user;
        }

        public bool Exist(string username)
        {
            if (_context.Users.Any(u => u.Username == username))
                return true;
            return false;
        }

        public User Find(Guid id)
        {
            return _context.Users.Find(id) ?? throw new Exception("User with id: " + id + " is not exist");
        }

        public object Get(int? status, Guid? roleId, int? pageIndex, int? pageSize)
        {
            var query = (from u in _context.Users
                         join r in _context.Roles on u.RoleId equals r.Id
                         select new
                         {
                             Id = u.Id,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             Address = u.Address,
                             Username = u.Username,
                             RoleId = u.RoleId,
                             Email = u.Email,
                             PhoneNumber = u.PhoneNumber,
                             CreateDate = u.CreateDate,
                             UpdateDate = u.UpdateDate,
                             Status = u.Status,
                             Balance = u.Balance,
                             RoleName = r.Name,
                         }).AsQueryable();

            if (status != null)
                query = query.Where(x => x.Status == status);

            if (roleId != null)
                query = query.Where(x => x.RoleId == roleId);

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            return query.OrderByDescending(od => od.CreateDate).ToList();
        }

        public void Insert(User user)
        {
            _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}