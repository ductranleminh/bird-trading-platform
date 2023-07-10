using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using bird_trading.Infrastructure.Utils.EndPoint;
using bird_trading.Infrastructure.Utils.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bird_trading.Api.Controllers
{
    [Route(ConfigEndPoint.Prefix + ConfigEndPoint.Version + "files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly S3Utils _utils;

        public FileController(S3Utils utils)
        {
            _utils = utils;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Insert(IFormFile fileInput, string fileExtension)
        {
            try
            {
                var file = _utils.UploadFile(fileInput, fileExtension);
                return await Task.FromResult(Ok(new {file}));

            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(new { status = "Fail", message = e.Message }));
            }
        }
    }
}