using bird_trading.Api.Entities;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Infrastructure.Utils.EndPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bird_trading.Api.Controllers
{
    [Route(ConfigEndPoint.Prefix + ConfigEndPoint.Version + "posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get(Guid? categoryId, Guid? packId, Guid? userId, int? status, int? pageIndex, int? pageSize)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.Get(categoryId, packId, userId, status, pageIndex, pageSize) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpGet("process")]
        public async Task<ActionResult> GetPostProcess(Guid? categoryId, Guid? packId, Guid? userId, int? pageIndex, int? pageSize)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.GetPostProcess(categoryId, packId, userId, pageIndex, pageSize) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpGet("{id}")]
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

        [HttpGet("banner")]
        public async Task<ActionResult> GetBanner(Guid? categoryId, Guid? userId, int? status, int? pageIndex, int? pageSize)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.GetPostBanner(categoryId, userId, status, pageIndex, pageSize) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Insert(PostEntityInsert entity)
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
        public async Task<ActionResult> ProcessRequest(PostEntityProcess entityProcess)
        {
            try
            {
                var command = _service.ResultTransaction(entityProcess);
                return await Task.FromResult(Ok(new { status = "Success", message = command }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update(Guid id, PostEntity entity)
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

        [HttpPut("status")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateStatus(PostEntityUpdateStatus entity)
        {
            try
            {
                var command = _service.UpdateStatus(entity);
                return await Task.FromResult(Ok(new { status = "Success", message = command }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }
    }
}