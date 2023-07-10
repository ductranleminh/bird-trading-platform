using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMediaService _mediaService;
        private readonly IPostTransactionService _postTransactionService;
        private PostValidator _validator = new PostValidator();
        public PostService(IPostRepository repository, IMediaService mediaService, IPostTransactionService postTransactionService)
        {
            _repository = repository;
            _mediaService = mediaService;
            _postTransactionService = postTransactionService;
        }
        public void Delete(Guid id)
        {
            var entity = _repository.Find(id);
            _repository.Delete(entity);
            _repository.Save();
        }

        public PostEntityDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(Guid? categoryId, Guid? packId, Guid? userId, int? status, int? pageIndex, int? pageSize)
        {
            return _repository.Get(categoryId, packId, userId, status, pageIndex, pageSize);
        }

        public object GetPostBanner(Guid? categoryId, Guid? userId, int? status, int? pageIndex, int? pageSize)
        {
            return _repository.GetPostBanner(categoryId, userId, status, pageIndex, pageSize);
        }

        public object GetPostProcess(Guid? categoryId, Guid? packId, Guid? userId, int? pageIndex, int? pageSize)
        {
            return _repository.GetPostProcess(categoryId, packId, userId, pageIndex, pageSize);
        }

        public string Insert(PostEntityInsert post)
        {
            post.Id = Guid.NewGuid();
            if (_validator.InsertPostValidator(post) != "Ok")
                return _validator.InsertPostValidator(post);

            var entity = new Post
            {
                Id = post.Id,
                CreateDate = DateTime.UtcNow.AddHours(7),
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
                Address = post.Address,
                PhoneSeller = post.PhoneSeller,
                NameSeller = post.NameSeller,
                CategoryId = post.CategoryId,
                UserId = post.UserId,
                Status = 0,
            };

            _repository.Insert(entity);
            _postTransactionService.PostInsert(post.postTransaction, entity.Id);
            _mediaService.InsertRange(post.medias, entity.Id);

            _repository.Save();
            return "Insert Success";
        }

        public string ResultTransaction(PostEntityProcess entityProcess)
        {
            var result = _repository.ProcessPost(entityProcess);
            _repository.Save();
            return result;
        }

        public string Update(Guid id, PostEntity entity)
        {
            var postUpdate = _repository.Find(id);

            if (_validator.UpdatePostValidator(entity) != "Ok")
                return _validator.UpdatePostValidator(entity);

            postUpdate.Title = entity.Title;
            postUpdate.Description = entity.Description;
            postUpdate.Price = entity.Price;
            postUpdate.Address = entity.Address;
            postUpdate.CategoryId = entity.CategoryId;
            postUpdate.UserId = entity.UserId;
            postUpdate.Status = entity.Status;

            _repository.Save();

            return "Update Success";
        }

        public string UpdateStatus(PostEntityUpdateStatus entity)
        {
            var postUpdate = _repository.Find(entity.postId);

            postUpdate.Status = entity.status;

            _repository.Save();

            return "Update Status Success";
        }
    }
}