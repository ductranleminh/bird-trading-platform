namespace bird_trading.Core.Services.Interfaces
{
    public interface IReportService
    {
        object GetPostByDay(DateTime startDate, DateTime endDate, Guid? userId, Guid? categoryId);
        object GetPostByMonth(int year, int month, Guid? userId, Guid? categoryId);
        object GetPostTransactionByDay(DateTime startDate, DateTime endDate, bool? isCancel);
        object GetPostTransactionByMonth(int year, int month, bool? isCancel);
        object GetTransactionByDay(DateTime startDate, DateTime endDate, Guid? userId);
        object GetTransactionByMonth(int year, int month, Guid? userId);
    }
}