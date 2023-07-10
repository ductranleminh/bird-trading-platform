using bird_trading.Api.Entities;
using bird_trading.Core.Services.Interfaces;
using bird_trading.Infrastructure.Utils.EndPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bird_trading.Api.Controllers
{
    [Route(ConfigEndPoint.Prefix + ConfigEndPoint.Version + "transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get(int? pageIndex, int? pageSize, int? status, Guid? userId)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.Get(pageIndex, pageSize, status, userId) }));
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

        [HttpPost("MakeRequest")]
        [Authorize]
        public async Task<ActionResult> MakeRequest(TransactionMakeRequest entity)
        {
            try
            {
                _service.MakeTransaction(entity);
                return await Task.FromResult(Ok(new { status = "Success", message = "Make Request Success", id = entity.Id }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpPost("ProcessRequest")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ProcessRequest(TransactionEntityProcess entityProcess)
        {
            try
            {
                var command = _service.ResultTransaction(entityProcess);
                return await Task.FromResult(Ok(new { status = "Success", message = command}));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }
    }
}