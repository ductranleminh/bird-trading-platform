using bird_trading.Core.Models;
using bird_trading.Api.Entities;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BirdContext _context;

        public TransactionRepository(BirdContext context)
        {
            _context = context;
        }
        public TransactionEntityDetail Detail(Guid id)
        {
            var transaction = (from t in _context.Transactions
                               where t.Id == id
                               select new TransactionEntityDetail
                               {
                                   Id = t.Id,
                                   Status = t.Status,
                                   Money = t.Money,
                                   PaymentType = t.PaymentType,
                                   CreateDate = t.CreateDate,
                                   UserId = t.UserId,
                                   Description = t.Description,
                               }).FirstOrDefault() ?? throw new KeyNotFoundException("Transaction is not exist");

            var user = (from u in _context.Users
                        where transaction.UserId == u.Id
                        select new TransactionEntityDetailUser
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Username = u.Username,
                            PhoneNumber = u.PhoneNumber,
                        }).FirstOrDefault() ?? throw new KeyNotFoundException("User is not exist");

            transaction.User = user;

            return transaction;
        }
        public object Get(int? pageIndex, int? pageSize, int? status, Guid? userId)
        {
            var query = (from t in _context.Transactions
                         select new
                         {
                             Id = t.Id,
                             Status = t.Status,
                             Money = t.Money,
                             PaymentType = t.PaymentType,
                             CreateDate = t.CreateDate,
                             UserId = t.UserId,
                             Description = t.Description,
                         }).AsQueryable();

            if (userId != null)
                query = query.Where(x => x.UserId == userId);

            if (status != null)
                query = query.Where(x => x.Status == status);

            if (pageIndex != null && pageSize != null)
                query = query.Skip(((int)pageIndex - 1) * (int)pageSize).Take((int)pageSize);

            return query.ToList();
        }

        public void Insert(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public string UpdateBalanceUser(TransactionEntityProcess entityProcess)
        {
            var transaction = (from t in _context.Transactions
                               where t.Id == entityProcess.TransactionId
                               select t).FirstOrDefault() ?? throw new Exception("Transaction with id: " + entityProcess.TransactionId + " is not exist");
            
            if (transaction.Status != 0)
                return "This transaction is approved or denided";

            var user = _context.Users.Find(transaction.UserId) ?? throw new Exception("User with id: " + transaction.UserId + " is not exist");

            if (entityProcess.type == 0) {
                transaction.Status = -1;
                return "Denide successful";
            }
                

            else if (entityProcess.type == 1) {
                if (transaction.PaymentType == 1) {
                    user.Balance = user.Balance + transaction.Money;
                    transaction.Status = 1;
                    return "Top-up successful";
                }
                
                else if (transaction.PaymentType == 0) {
                    if (user.Balance < transaction.Money)
                        throw new Exception("User is not enough balance to withdraw");

                    user.Balance = user.Balance - transaction.Money;
                    transaction.Status = 1;
                    return "Withdraw successful";
                }
                else
                    return "Invalid argument payment type";
            }

            else
                return  "Invalid argument type";
        }
    }
}