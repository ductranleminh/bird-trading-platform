using bird_trading.Core.Services.Interfaces;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;
        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }
        public object GetPostByDay(DateTime startDate, DateTime endDate, Guid? userId, Guid? categoryId)
        {
            return _repository.GetPostByDay(startDate, endDate, userId, categoryId);
        }

        public object GetPostByMonth(int year, int month, Guid? userId, Guid? categoryId)
        {
            return _repository.GetPostByMonth(year, month, userId, categoryId);
        }

        public object GetPostTransactionByDay(DateTime startDate, DateTime endDate, bool? isCancel)
        {
            return _repository.GetPostTransactionByDay(startDate, endDate, isCancel);
        }

        public object GetPostTransactionByMonth(int year, int month, bool? isCancel)
        {
            return _repository.GetPostTransactionByMonth(year, month, isCancel);
        }

        public object GetTransactionByDay(DateTime startDate, DateTime endDate, Guid? userId)
        {
            return _repository.GetTransactionByDay(startDate, endDate, userId);
        }

        public object GetTransactionByMonth(int year, int month, Guid? userId)
        {
            return _repository.GetTransactionByMonth(year, month, userId);
        }
    }
}