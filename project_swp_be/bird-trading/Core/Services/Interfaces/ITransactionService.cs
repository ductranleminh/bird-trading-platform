using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface ITransactionService
    {
        object Get(int? pageIndex, int? pageSize, int? status, Guid? userId);
        TransactionEntityDetail Detail(Guid id);
        void MakeTransaction(TransactionMakeRequest request);
        string ResultTransaction(TransactionEntityProcess entityProcess);
    }
}