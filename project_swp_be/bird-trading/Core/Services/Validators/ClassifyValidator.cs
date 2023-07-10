using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Validators
{
    public class ClassifyValidator
    {
        public ClassifyValidator()
        {
        }

        public string AClassifyValidator(ClassifyEntity classify)
        {
            if (classify.Name.Length > 50)
                 return "Name max length is 50";

            return "Ok";
        }
    }
}