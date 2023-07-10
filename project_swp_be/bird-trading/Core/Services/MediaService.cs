using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _repository;
        private MediaValidator _validator = new MediaValidator();
        public MediaService(IMediaRepository repository)
        {
            _repository = repository;
        }
        public void Delete(Guid id)
        {
            var media = _repository.Find(id);
            _repository.Delete(media);
            _repository.Save();
        }

        public MediaEntityDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(Guid? postId, int? pageIndex, int? pageSize)
        {
            return _repository.Get(postId, pageIndex, pageSize);
        }

        public string Insert(MediaEntity media)
        {
            media.Id = Guid.NewGuid();

            if (_validator.AMediaValidator(media) != "Ok")
                return _validator.AMediaValidator(media);

            var entity = new Media
            {
                Id = media.Id,
                Url = media.Url,
                PostId = media.PostId
            };

            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string InsertRange(List<PostEntityInsertMedias> medias, Guid postId)
        {
            List<Media> listMedia = new List<Media>();
            foreach (var x in medias)
            {
                x.Id = Guid.NewGuid();

                if (_validator.PostMediaValidator(x) != "Ok")
                    return _validator.PostMediaValidator(x);

                var entity = new Media
                {
                    Id = x.Id,
                    Url = x.Url,
                    PostId = postId,
                    Extension = x.Extension,
                };
                listMedia.Add(entity);
            }

            _repository.InsertRange(listMedia);
            _repository.Save();

            return "Insert Range Success";
        }

        public string Update(Guid id, MediaEntity media)
        {
            var mediaUpdate = _repository.Find(id);

            if (_validator.AMediaValidator(media) != "Ok")
                return _validator.AMediaValidator(media);

            mediaUpdate.Url = media.Url;
            mediaUpdate.PostId = media.PostId;
            mediaUpdate.Extension = media.Extension;

            _repository.Save();

            return "Update Success";
        }
    }
}