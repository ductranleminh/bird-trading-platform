using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Core.Services.Interfaces
{
    public interface IMediaService
    {
        object Get(Guid? postId, int? pageIndex, int? pageSize);
        MediaEntityDetail Detail(Guid id);
        string Insert(MediaEntity media);
        string InsertRange(List<PostEntityInsertMedias> medias, Guid postId);
        string Update(Guid id, MediaEntity media);
        void Delete(Guid id);
    }
}