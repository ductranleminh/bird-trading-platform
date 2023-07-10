using bird_trading.Api.Entities;
using bird_trading.Core.Models;

namespace bird_trading.Data.Repositories.Interfaces
{
    public interface IBankInfomationRepository
    {
        object Get(Guid? userId);
        BankInfomationEntityDetail Detail(Guid id);
        BankInfomation Find(Guid id);
        void Insert(BankInfomation bankInfomation);
        void Delete(BankInfomation bankInfomation);
        void Save();
    }
}