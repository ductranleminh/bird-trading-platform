using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;
using bird_trading.Infrastructure.Extensions.Security;

namespace bird_trading.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private UserValidator _validator = new UserValidator();
        private HashPassword _security = new HashPassword();
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Delete(Guid id)
        {
            var user = _repository.Find(id);
            _repository.Delete(user);
            _repository.Save();
        }

        public GetUserEntityDetail Detail(Guid id)
        {
            return _repository.Detail(id);
        }

        public object Get(int? status, Guid? roleId, int? pageIndex, int? pageSize)
        {
            return _repository.Get(status, roleId, pageIndex, pageSize);
        }

        public string Insert(InsertUserEntity user)
        {
            user.Id = Guid.NewGuid();
            if (_repository.Exist(user.Username))
                return "Username is exist";

            if (_validator.InsertValidator(user) != "Ok")
                return _validator.InsertValidator(user);

            var entity = new User {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Username = user.Username.ToLower(),
                Password = _security.EncryptPass(user.Password),
                RoleId = user.RoleId,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreateDate = DateTime.UtcNow.AddHours(7),
                UpdateDate = DateTime.UtcNow.AddHours(7),
                Status = 1,
                Balance = user.Balance,
            };

            _repository.Insert(entity);
            _repository.Save();

            return "Insert Success";
        }

        public string Update(Guid id, UpdateUserEntity user)
        {
            var userUpdate = _repository.Find(id);

            if (_validator.UpdateValidator(user) != "Ok")
                return _validator.UpdateValidator(user);

            userUpdate.FirstName = user.FirstName;
            userUpdate.LastName = user.LastName;
            userUpdate.Address = user.Address;
            userUpdate.Email = user.Email;
            userUpdate.PhoneNumber = user.PhoneNumber;
            userUpdate.UpdateDate = DateTime.UtcNow.AddHours(7);

            _repository.Save();

            return "Update Success";
        }

        public decimal UpdateBalance(UpdateBalanceUserEntity entity)
        {
            var userUpdate = _repository.Find(entity.Id);

            userUpdate.Balance = userUpdate.Balance + entity.money;

            _repository.Save();

            return userUpdate.Balance;
        }
    }
}