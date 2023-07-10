using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        object Get(int? pageIndex, int? pageSize, int? status, Guid? userId);
        TransactionEntityDetail Detail(Guid id);
        void Insert(Transaction transaction);
        string UpdateBalanceUser(TransactionEntityProcess entityProcess);
        void Save();
    }
}