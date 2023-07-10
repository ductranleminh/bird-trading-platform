namespace bird_trading.Api.Entities
{
    public class FileEntity
    {
        public IFormFile file { get; set; } = null!;
        public string fileExtension { get; set; } = null!;
    }
}