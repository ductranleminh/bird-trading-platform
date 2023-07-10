using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Core.Services.Validators;
using bird_trading.Data.Repositories.Interfaces;
using bird_trading.Infrastructure.Extensions.Security;

namespace bird_trading.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private AuthValidator _validator = new AuthValidator();
        private HashPassword _security = new HashPassword();
        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        public string ChangePassword(AuthEntityChangePassword entity)
        {
            entity.NewPassword = _security.EncryptPass(entity.NewPassword);
            var command = _repository.ChangePassword(entity);
            _repository.Save();
            return command;
        }

        public string? Login(LoginEntity login)
        {
            return _repository.Login(login);
        }

        public string Resgister(UserEntity user)
        {
            user.Id = Guid.NewGuid();
            if (_repository.Exist(user.Username))
                return "Username is exist";

            if (_validator.RegisterValidator(user) != "Ok")
                return _validator.RegisterValidator(user);

            var entity = new User {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Username = user.Username.ToLower(),
                Password = _security.EncryptPass(user.Password),
                RoleId = _repository.GetRoleUser(),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreateDate = DateTime.UtcNow.AddHours(7),
                UpdateDate = DateTime.UtcNow.AddHours(7),
                Status = 1,
                Balance = 0,
            };
            _repository.Register(entity);
            _repository.Save();

            return "Register Success";
        }
    }
}

