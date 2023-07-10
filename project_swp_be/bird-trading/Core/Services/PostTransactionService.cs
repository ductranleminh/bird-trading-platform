using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class PostTransactionService : IPostTransactionService
    {
        private readonly IPostTransactionRepository _repository;
        private PostTransactionValidator _validator = new PostTransactionValidator();
        public PostTransactionService(IPostTransactionRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Guid id)
        {
            var postTransaction = _repository.Find(id);
            _repository.Delete(postTransaction);
            _repository.Save();
        }

        public PostTransactionEntityDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(Guid? postId, Guid? packId, bool? IsCancel, int? pageIndex, int? pageSize)
        {
            return _repository.Get(postId, packId, IsCancel, pageIndex, pageSize);
        }

        public string Insert(PostTransactionEntity postTransaction)
        {
            postTransaction.Id = Guid.NewGuid();

            if (_validator.APackValidator(postTransaction) != "Ok")
                return _validator.APackValidator(postTransaction);

            var entity = new PostTransaction
            {
                Id = postTransaction.Id,
                Price = postTransaction.Price,
                CreateDate = DateTime.UtcNow.AddHours(7),
                EffectDate = postTransaction.EffectDate,
                ExpiredDay = postTransaction.ExpiredDay,
                IsCancel = false,
                PackId = postTransaction.PackId,
                PostId = postTransaction.PostId,
            };

            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string PostInsert(PostEntityInsertPostTransaction postTransaction, Guid postId)
        {
            postTransaction.Id = Guid.NewGuid();

            if (_validator.APackValidator(postTransaction) != "Ok")
                return _validator.APackValidator(postTransaction);

            var entity = new PostTransaction
            {
                Id = postTransaction.Id,
                Price = postTransaction.Price,
                CreateDate = DateTime.UtcNow.AddHours(7),
                EffectDate = postTransaction.EffectDate.AddHours(7),
                ExpiredDay = postTransaction.ExpiredDay,
                IsCancel = true,
                PackId = postTransaction.PackId,
                PostId = postId,
            };

            var processEntity = new PostTransactionEntityProcess {
                postTransactionId = postTransaction.Id,
                type = 1,
            };
            //_repository.ProcessPostTransaction(processEntity);
            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string ResultPostTransaction(PostTransactionEntityProcess entityProcess)
        {
            var result = _repository.ProcessPostTransaction(entityProcess);
            _repository.Save();
            return result;
        }

        public string Update(Guid id, PostTransactionEntity postTransaction)
        {
            var postTransactionUpdate = _repository.Find(id);

            if (_validator.APackValidator(postTransaction) != "Ok")
                return _validator.APackValidator(postTransaction);

            postTransactionUpdate.Price = postTransaction.Price;
            postTransactionUpdate.EffectDate = postTransaction.EffectDate;
            postTransactionUpdate.ExpiredDay = postTransaction.ExpiredDay;
            postTransactionUpdate.PackId = postTransaction.PackId;
            postTransactionUpdate.PostId = postTransaction.PostId;

            _repository.Save();

            return "Update Success";
        }

        public string UpdateIsCancel(PostTransactionEntityIsCancel entity)
        {
            var postTransactionUpdate = _repository.Find(entity.postTransactionId);

            postTransactionUpdate.IsCancel = entity.IsCancel;

            _repository.Save();

            return "Update Success";
        }
    }
}