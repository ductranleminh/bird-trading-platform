using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface IMediaRepository
    {
        object Get(Guid? postId, int? pageIndex, int? pageSize);
        MediaEntityDetail Detail(Guid id);
        Media Find(Guid id);
        void Insert(Media media);
        void InsertRange(List<Media> medias);
        void Delete(Media media);
        void Save();
    }
}