using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface IPostTransactionService
    {
        object Get(Guid? postId, Guid? packId, bool? IsCancel, int? pageIndex, int? pageSize);
        string ResultPostTransaction(PostTransactionEntityProcess entityProcess);
        PostTransactionEntityDetail Detail(Guid id);
        string Insert(PostTransactionEntity postTransaction);
        string PostInsert(PostEntityInsertPostTransaction postTransaction, Guid postId);
        string Update(Guid id, PostTransactionEntity postTransaction);
        string UpdateIsCancel(PostTransactionEntityIsCancel entity);
        void Delete(Guid id);
    }
}