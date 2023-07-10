using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Validators
{
    public class BankInfomationValidator
    {

        public BankInfomationValidator()
        {
        }

        public string ABankInfomationValidator(BankInfomationEntity bankInfomation)
        {
            if (bankInfomation.Name.Length > 200)
                return "Name max length is 200";

            return "Ok";
        }
    }
}