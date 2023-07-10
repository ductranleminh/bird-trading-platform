using bird_trading.Api.Entities;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;
using bird_trading.Infrastructure.Extensions.Authorize;
using bird_trading.Infrastructure.Extensions.Security;

namespace bird_trading.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly BirdContext _context;
        private readonly IConfiguration _config;
        private HashPassword _security = new HashPassword();

        public AuthRepository(BirdContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string ChangePassword(AuthEntityChangePassword entity)
        {
            var user = (from u in _context.Users
                        where u.Username == entity.Username
                        select u).FirstOrDefault();

            if (user == null || !_security.DecryptPass(entity.OldPassword ?? "", user.Password))
                throw new Exception("Username or Oldpassword is not correct");

            user.Password = entity.NewPassword;
            //_context.SaveChanges();

            return "Change Password success";
        }

        public bool Exist(string username)
        {
            if (_context.Users.Any(u => u.Username == username))
                return true;
            return false;
        }

        public Guid GetRoleUser()
        {
            var Id = (from r in _context.Roles
                      where r.Name == "user"
                      select r.Id).FirstOrDefault();

            return Id;
        }

        public string? Login(LoginEntity login)
        {
            var user = (from u in _context.Users
                        join r in _context.Roles on u.RoleId equals r.Id
                        where u.Username == login.Username
                        select new User
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Address = u.Address,
                            Username = u.Username,
                            Password = u.Password,
                            RoleId = u.RoleId,
                            Email = u.Email,
                            PhoneNumber = u.PhoneNumber,
                            CreateDate = u.CreateDate,
                            UpdateDate = u.UpdateDate,
                            Status = u.Status,
                            Balance = u.Balance,
                            Role = r,
                        }).FirstOrDefault();

            if (user == null || !_security.DecryptPass(login.Password ?? "", user.Password))
                throw new Exception("Username or password is not correct");

            TokenJwt tokenJwt = new TokenJwt(_config);
            var tokenString = tokenJwt.GenerateJwtToken(user);

            return tokenString;
        }

        public void Register(User user)
        {
            var userCheck = (from u in _context.Users
                             where u.Username == user.Username
                             select u).FirstOrDefault();

            if (userCheck is not null)
                throw new Exception("Username is exist");

            _context.Users.Add(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}