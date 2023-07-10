using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using bird_trading.Core.Models;
using bird_trading.Data.Contexts;
using bird_trading.Infrastructure.Utils.EndPoint;
using Microsoft.AspNetCore.Mvc;

namespace bird_trading.Api.Controllers
{
    [ApiController]
    [Route(ConfigEndPoint.Prefix + ConfigEndPoint.Version + "init")]
    public class InitController : ControllerBase
    {
        private readonly BirdContext _context;
        public InitController(BirdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Init()
        {
            try
            {
                var query = (from r in _context.Roles
                             select r).ToList();

                if (query.Count() != 0)
                    return await Task.FromResult(Ok(new { result = "Success", message = "Project Inited" }));

                var role1 = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "admin"
                };
                var role2 = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "user"
                };
                _context.Roles.Add(role1);
                _context.Roles.Add(role2);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Tran",
                    LastName = "Duyen",
                    Address = "Ho Chi Minh",
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("123"),
                    RoleId = role1.Id,
                    Email = "duyen@domain.com",
                    PhoneNumber = "0123456789",
                    CreateDate = DateTime.UtcNow.AddHours(7),
                    UpdateDate = DateTime.UtcNow.AddHours(7),
                    Status = 1,
                    Balance = 10000,
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                return await Task.FromResult(Ok(new { result = "Success", message = "Init Success" }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }
    }
}