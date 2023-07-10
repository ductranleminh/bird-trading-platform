using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Validators
{
    public class PostValidator
    {
        public PostValidator()
        {
        }

        public string InsertPostValidator(PostEntityInsert entity)
        {
            if (entity.Title.Length > 200)
                return "Title max length is 200";

            if (entity.Price < 0)
                return "Price must be positive";

            return "Ok";
        }

        public string UpdatePostValidator(PostEntity entity)
        {
            if (entity.Title.Length > 200)
                return "Title max length is 200";

            if (entity.Price < 0)
                return "Price must be positive";

            return "Ok";
        }
    }
}