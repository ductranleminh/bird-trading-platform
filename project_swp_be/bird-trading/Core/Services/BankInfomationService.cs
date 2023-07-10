using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class BankInfomationService : IBankInfomationService
    {
        private readonly IBankInfomationRepository _repository;
        private BankInfomationValidator _validator = new BankInfomationValidator();
        public BankInfomationService(IBankInfomationRepository repository)
        {
            _repository = repository;
        }
        public void Delete(Guid id)
        {
            var bankInfomation = _repository.Find(id);
            _repository.Delete(bankInfomation);
            _repository.Save();
        }

        public BankInfomationEntityDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(Guid? userId)
        {
            return _repository.Get(userId);
        }

        public string Insert(BankInfomationEntity bankInfomation)
        {
            bankInfomation.Id = Guid.NewGuid();

            if (_validator.ABankInfomationValidator(bankInfomation) != "Ok")
                return _validator.ABankInfomationValidator(bankInfomation);

            var entity = new BankInfomation
            {
                Id = bankInfomation.Id,
                UserId = bankInfomation.UserId,
                Name = bankInfomation.Name,
                AccountNumber = bankInfomation.AccountNumber,
            };

            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string Update(Guid id, BankInfomationEntity bankInfomation)
        {
            var bankInfomationUpdate = _repository.Find(id);

            if (_validator.ABankInfomationValidator(bankInfomation) != "Ok")
                return _validator.ABankInfomationValidator(bankInfomation);

            bankInfomationUpdate.UserId = bankInfomation.UserId;
            bankInfomationUpdate.Name = bankInfomation.Name;
            bankInfomationUpdate.AccountNumber = bankInfomation.AccountNumber;

            _repository.Save();

            return "Update Success";
        }
    }
}