using bird_trading.Api.Entities;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Infrastructure.Utils.EndPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bird_trading.Api.Controllers
{
    [Route(ConfigEndPoint.Prefix + ConfigEndPoint.Version + "news")]
    [ApiController]
    public class NewController : ControllerBase
    {
        private readonly INewService _service;

        public NewController(INewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get(Guid? classifyId, Guid? userId, int? pageIndex, int? pageSize)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.Get(classifyId, userId, pageIndex, pageSize) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Detail(Guid id)
        {
            try
            {
                var command = _service.Detail(id);

                return await Task.FromResult(Ok(new { status = "Success", data = command }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Insert(NewEntity entity)
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

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Update(Guid id, NewEntity entity)
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