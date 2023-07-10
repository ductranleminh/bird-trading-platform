using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Validators
{
    public class MediaValidator
    {
        public MediaValidator()
        {
        }

        public string AMediaValidator(MediaEntity media)
        {
            if (media.Extension.Length > 50)
                return "Extension max length is 10";

            return "Ok";
        }

        public string PostMediaValidator(PostEntityInsertMedias media)
        {
            if (media.Extension.Length > 50)
                return "Extension max length is 10";

            return "Ok";
        }
    }
}