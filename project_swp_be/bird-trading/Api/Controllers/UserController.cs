using bird_trading.Api.Entities;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Infrastructure.Utils.EndPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bird_trading.Api.Controllers
{
    [Route(ConfigEndPoint.Prefix + ConfigEndPoint.Version + "users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Get(int? status, Guid? roleId, int? pageIndex, int? pageSize)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.Get(status, roleId, pageIndex, pageSize) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Detail(Guid id)
        {
            try
            {
                var user = _service.Detail(id);

                return await Task.FromResult(Ok(new { status = "Success", data = user }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Insert(InsertUserEntity entity)
        {
            try
            {
                var command = _service.Insert(entity);
                if (command != "Insert Success")
                    return await Task.FromResult(Ok(new { status = "Fail", message = command }));
                return await Task.FromResult(Ok(new { status = "Success", message = command, id = entity.Id }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpPut("balance")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateBalance(UpdateBalanceUserEntity entity)
        {
            try
            {
                var command = _service.UpdateBalance(entity);
                return await Task.FromResult(Ok(new { status = "Success", balance = command }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update(Guid id, UpdateUserEntity entity)
        {
            try
            {
                var command = _service.Update(id, entity);
                if (command != "Update Success")
                    return await Task.FromResult(Ok(new { status = "Fail", message = command }));
                return await Task.FromResult(Ok(new { status = "Success", message = command }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                _service.Delete(id);

                return await Task.FromResult(Ok(new { status = "Success", message = "Delete Success" }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }
    }
}