using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Validators
{
    public class NewValidator
    {
        public NewValidator()
        {
        }

        public string ANewValidator(NewEntity news)
        {
            if (news.Title.Length > 200)
                return "Title max length is 200";

            return "Ok";
        }
    }
}