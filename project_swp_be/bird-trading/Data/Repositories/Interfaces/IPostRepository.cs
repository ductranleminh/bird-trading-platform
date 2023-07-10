using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        object Get(Guid? categoryId, Guid? packId, Guid? userId, int? status, int? pageIndex, int? pageSize);
        object GetPostProcess(Guid? categoryId, Guid? packId, Guid? userId, int? pageIndex, int? pageSize);
        object GetPostBanner(Guid? categoryId, Guid? userId, int? status, int? pageIndex, int? pageSize);
        string ProcessPost(PostEntityProcess entityProcess);
        PostEntityDetail Detail(Guid id);
        Post Find(Guid id);
        void Insert(Post post);
        void Delete(Post post);
        void Save();
    }
}