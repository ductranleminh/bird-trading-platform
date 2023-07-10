using bird_trading.Api.Entities;

namespace bird_trading.Core.Services.Interfaces
{
    public interface IBankInfomationService
    {
        object Get(Guid? userId);
        BankInfomationEntityDetail Detail(Guid id);
        string Insert(BankInfomationEntity bankInfomation);
        string Update(Guid id, BankInfomationEntity bankInfomation);
        void Delete(Guid id);
    }
}