using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace bird_trading.Infrastructure.Utils.S3
{
    public class S3Utils
    {
        private readonly IConfiguration _config;

        public S3Utils(IConfiguration config)
        {
            _config = config;
        }
        public async Task<object> UploadFile(IFormFile file, string fileExtension)
        {

            try
            {
                var accessKey = _config.GetValue<string>("CloudflareString:AccessKey");
                var secretKey = _config.GetValue<string>("CloudflareString:SecretKey");
                var credentials = new BasicAWSCredentials(accessKey, secretKey);
                var fileName = Guid.NewGuid();
                IAmazonS3 s3Client = new AmazonS3Client(credentials, new AmazonS3Config
                {
                    ServiceURL = _config.GetValue<string>("CloudflareString:ServiceURL"),
                });

                var request = new PutObjectRequest()
                {
                    InputStream = file.OpenReadStream(),
                    Key = fileName.ToString() + "." + fileExtension,
                    BucketName = "cloudflare",
                    DisablePayloadSigning = true
                };

                request.Metadata.Add("Content-Type", file.ContentType);
                var response = await s3Client.PutObjectAsync(request);
                string url = _config.GetValue<string>("CloudflareString:PublicURL") + fileName.ToString() + "." + fileExtension;

                return url;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}