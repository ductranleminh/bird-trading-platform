using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class BankInfomationRepository : IBankInfomationRepository
    {
        private readonly BirdContext _context;

        public BankInfomationRepository(BirdContext context)
        {
            _context = context;
        }

        public void Delete(BankInfomation bankInfomation)
        {
            _context.BankInfomations.Remove(bankInfomation);
        }

        public BankInfomationEntityDetail Detail(Guid id)
        {
            var query = (from bi in _context.BankInfomations
                         where bi.Id == id
                         select new BankInfomationEntityDetail
                         {
                             Id = bi.Id,
                             UserId = bi.UserId,
                             Name = bi.Name,
                             AccountNumber = bi.AccountNumber,
                         }).FirstOrDefault() ?? throw new Exception("Bank with id: " + id + " is not exist");

            query.User = (from u in _context.Users
                        where u.Id == query.UserId
                        select new BankInfomationEntityDetailUser
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Username = u.Username,
                            PhoneNumber = u.PhoneNumber,
                        }).FirstOrDefault() ?? throw new Exception("User with id: " + id + " is not exist");

            return query;
        }

        public BankInfomation Find(Guid id)
        {
            return _context.BankInfomations.Find(id) ?? throw new Exception("Bank with id: " + id + " is not exist");
        }

        public object Get(Guid? userId)
        {
            var query = (from bi in _context.BankInfomations
                         select new
                         {
                             Id = bi.Id,
                             UserId = bi.UserId,
                             Name = bi.Name,
                             AccountNumber = bi.AccountNumber,
                         }).AsQueryable();

            if (userId != null)
                query = query.Where(w => w.UserId == userId);

            return query.ToList();
        }

        public void Insert(BankInfomation bankInfomation)
        {
            _context.BankInfomations.Add(bankInfomation);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}