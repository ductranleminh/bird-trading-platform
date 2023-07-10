using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }
        public TransactionEntityDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(int? pageIndex, int? pageSize, int? status, Guid? userId)
        {
            return _repository.Get(pageIndex, pageSize, status, userId);
        }

        public void MakeTransaction(TransactionMakeRequest request)
        {
            if (request.PaymentType != 1 && request.PaymentType != 0)
                throw new Exception("Invalid arrgument payment type");

            request.Id = Guid.NewGuid();

            var entity = new Transaction{
                Id = request.Id,
                Status = 0,
                Money = request.Money,
                PaymentType = request.PaymentType,
                CreateDate = DateTime.UtcNow.AddHours(7),
                UserId = request.UserId,
                Description = request.Description,
            };

            _repository.Insert(entity);
            _repository.Save();
        }

        public string ResultTransaction(TransactionEntityProcess entityProcess)
        {
            var result = _repository.UpdateBalanceUser(entityProcess);
            _repository.Save();
            return result;
        }
    }
}