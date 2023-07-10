using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Validators
{
    public class CategoryValidator
    {
        public CategoryValidator()
        {
        }

        public string ACategoryValidator(CategoryEntity category)
        {
            if (category.Title.Length > 200)
                return "Title max length is 200";

            return "Ok";
        }
    }
}