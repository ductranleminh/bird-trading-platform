using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Validators
{
    public class PackValidator
    {
        public PackValidator()
        {
        }

        public string APackValidator(PackEntity pack)
        {
            if (pack.Title.Length > 200)
                return "Title max length is 200";

            return "Ok";
        }
    }
}