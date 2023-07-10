using bird_trading.Core.Services.Interfaces;
using bird_trading.Infrastructure.Utils.EndPoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bird_trading.Api.Controllers
{
    [Route(ConfigEndPoint.Prefix + ConfigEndPoint.Version + "reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("GetPostByDay")]
        [Authorize]
        public async Task<ActionResult> GetPostByDay(DateTime startDate, DateTime endDate, Guid? userId, Guid? categoryId)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.GetPostByDay(startDate, endDate, userId, categoryId) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpGet("GetPostByMonth")]
        [Authorize]
        public async Task<ActionResult> GetPostByMonth(int year, int month, Guid? userId, Guid? categoryId)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.GetPostByMonth(year, month, userId, categoryId) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpGet("GetPostTransactionByDay")]
        [Authorize]
        public async Task<ActionResult> GetPostTransactionByDay(DateTime startDate, DateTime endDate, bool? isCancel)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.GetPostTransactionByDay(startDate, endDate, isCancel) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpGet("GetPostTransactionByMonth")]
        [Authorize]
        public async Task<ActionResult> GetPostTransactionByMonth(int year, int month, bool? isCancel)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.GetPostTransactionByMonth(year, month, isCancel) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpGet("GetTransactionByDay")]
        [Authorize]
        public async Task<ActionResult> GetTransactionByDay(DateTime startDate, DateTime endDate, Guid? userId)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.GetTransactionByDay(startDate, endDate, userId) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }

        [HttpGet("GetTransactionByMonth")]
        [Authorize]
        public async Task<ActionResult> GetTransactionByMonth(int year, int month, Guid? userId)
        {
            try
            {
                return await Task.FromResult(Ok(new { status = "Success", data = _service.GetTransactionByMonth(year, month, userId) }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { result = "Fail", message = e.Message }));
            }
        }
    }
}