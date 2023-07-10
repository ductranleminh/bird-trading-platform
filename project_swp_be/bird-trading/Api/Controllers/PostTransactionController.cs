using bird_trading.Api.Entities;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Infrastructure.Utils.EndPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bird_trading.Api.Controllers
{
    [Route(ConfigEndPoint.Prefix + ConfigEndPoint.Version + "post-transactions")]
    [ApiController]
    public class PostTransactionController : ControllerBase
    {
        private readonly IPostTransactionService _service;

        public PostTransactionController(IPostTransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get(Guid? postId, Guid? packId, bool? IsCancel, int? pageIndex, int? pageSize)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.Get(postId, packId, IsCancel, pageIndex, pageSize) }));
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
                var command = _service.Detail(id);

                return await Task.FromResult(Ok(new { status = "Success", data = command }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Insert(PostTransactionEntity entity)
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

        [HttpPost("ProcessRequest")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ProcessRequest(PostTransactionEntityProcess entityProcess)
        {
            try
            {
                var command = _service.ResultPostTransaction(entityProcess);
                return await Task.FromResult(Ok(new { status = "Success", message = command }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpPut("IsCancel")]
        [Authorize]
        public async Task<ActionResult> UpdateIsCancel(PostTransactionEntityIsCancel entity)
        {
            try
            {
                var command = _service.UpdateIsCancel(entity);
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