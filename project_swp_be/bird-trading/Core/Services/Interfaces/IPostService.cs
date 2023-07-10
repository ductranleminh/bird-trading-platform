using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface IPostService
    {
        object Get(Guid? categoryId, Guid? packId, Guid? userId, int? status, int? pageIndex, int? pageSize);
        object GetPostProcess(Guid? categoryId, Guid? packId, Guid? userId, int? pageIndex, int? pageSize);
        object GetPostBanner(Guid? categoryId, Guid? userId, int? status, int? pageIndex, int? pageSize);
        string ResultTransaction(PostEntityProcess entityProcess);
        PostEntityDetail Detail(Guid id);
        string Insert(PostEntityInsert post);
        string Update(Guid id, PostEntity pack);
        string UpdateStatus(PostEntityUpdateStatus entity);
        void Delete(Guid id);
    }
}