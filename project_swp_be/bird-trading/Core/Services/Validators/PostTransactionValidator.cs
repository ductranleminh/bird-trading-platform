using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Validators
{
    public class PostTransactionValidator
    {
        public PostTransactionValidator()
        {
        }

        public string APackValidator(PostTransactionEntity postTransaction)
        {
            if (postTransaction.Price <= 0)
                return "Price must be positive";

            if (postTransaction.ExpiredDay <= 0)
                return "Exprired Day must be positive";

            return "Ok";
        }

        public string APackValidator(PostEntityInsertPostTransaction postTransaction)
        {
            if (postTransaction.Price <= 0)
                return "Price must be positive";

            if (postTransaction.ExpiredDay <= 0)
                return "Exprired Day must be positive";

            return "Ok";
        }
    }
}