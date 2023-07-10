using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface IPostTransactionRepository
    {
        object Get(Guid? postId, Guid? packId, bool? IsCancel, int? pageIndex, int? pageSize);
        string ProcessPostTransaction(PostTransactionEntityProcess entityProcess);
        PostTransactionEntityDetail Detail(Guid id);
        PostTransaction Find(Guid id);
        void Insert(PostTransaction postTransaction);
        void Delete(PostTransaction postTransaction);
        void Save();
    }
}