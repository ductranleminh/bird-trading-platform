using bird_trading.Data.Contexts;
using bird_trading.Data.Repositories.Interfaces;

namespace bird_trading.Data.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly BirdContext _context;

        public ReportRepository(BirdContext context)
        {
            _context = context;
        }
        public object GetPostByDay(DateTime startDate, DateTime endDate, Guid? userId, Guid? categoryId)
        {
            var query = _context.Posts
                        .Where(w => w.CreateDate >= startDate && w.CreateDate <= endDate)
                        .AsQueryable();

            if (userId != null)
                query = query.Where(w => w.UserId == userId);

            if (categoryId != null)
                query = query.Where(w => w.CategoryId == categoryId);

            object post = new
            {
                totalPostApprove = query.Where(w => w.Status == 1).Count(),
                totalPostProcessing = query.Where(w => w.Status == 0).Count(),
                totalPostDenied = query.Where(w => w.Status == -1).Count(),
            };

            return post;
        }

        public object GetPostByMonth(int year, int month, Guid? userId, Guid? categoryId)
        {
            var query = _context.Posts
                        .Where(w => w.CreateDate >= new DateTime(year, month, 1) && w.CreateDate <= new DateTime(year, month, DateTime.DaysInMonth(year, month)))
                        .AsQueryable();

            if (userId != null)
                query = query.Where(w => w.UserId == userId);

            if (categoryId != null)
                query = query.Where(w => w.CategoryId == categoryId);

            object post = new
            {
                totalPostApprove = query.Where(w => w.Status == 1).Count(),
                totalPostProcessing = query.Where(w => w.Status == 0).Count(),
                totalPostDenied = query.Where(w => w.Status == -1).Count(),
            };

            return post;
        }

        public object GetPostTransactionByDay(DateTime startDate, DateTime endDate, bool? isCancel)
        {
            var query = _context.PostTransactions
                        .Where(w => w.CreateDate >= startDate && w.CreateDate <= endDate)
                        .AsQueryable();

            if (isCancel != null)
                query = query.Where(w => w.IsCancel == isCancel);

            object postTransaction = new
            {
                TotalMoney = query.Sum(s => s.Price),
                TotalTransaction = query.Count(),
                TotalUsing = query.Where(w => w.EffectDate <= DateTime.UtcNow.AddHours(7)).Count(),
            };

            return postTransaction;
        }

        public object GetPostTransactionByMonth(int year, int month, bool? isCancel)
        {
            var query = _context.PostTransactions
            .Where(w => w.CreateDate >= new DateTime(year, month, 1) && w.CreateDate <= new DateTime(year, month, DateTime.DaysInMonth(year, month)))
            .AsQueryable();

            if (isCancel != null)
                query = query.Where(w => w.IsCancel == isCancel);

            object postTransaction = new
            {
                TotalMoney = query.Sum(s => s.Price),
                TotalTransaction = query.Count(),
                TotalUsing = query.Where(w => w.EffectDate <= DateTime.UtcNow.AddHours(7)).Count(),
            };

            return postTransaction;
        }

        public object GetTransactionByDay(DateTime startDate, DateTime endDate, Guid? userId)
        {
            var query = _context.Transactions
                        .Where(w => w.CreateDate >= startDate && w.CreateDate <= endDate)
                        .AsQueryable();

            if (userId != null)
                query = query.Where(w => w.UserId == userId);

            object transaction = new
            {
                TotalProcessing = query.Where(w => w.Status == 0).Count(),
                TotalTopup = query.Where(w => w.Status == 1 && w.PaymentType == 1).Count(),
                TotalMoneyTopup = query.Where(w => w.Status == 1 && w.PaymentType == 1).Sum(s => s.Money),
                TotalWithdraw = query.Where(w => w.Status == 1 && w.PaymentType == 0).Count(),
                TotalMoneyWithdraw = query.Where(w => w.Status == 1 && w.PaymentType == 0).Sum(s => s.Money),
                TotalDenied = query.Where(w => w.Status == -1 && w.PaymentType == 0).Count(),
                TotalMoneyDenied = query.Where(w => w.Status == -1 && w.PaymentType == 0).Sum(s => s.Money),
            };

            return transaction;
        }

        public object GetTransactionByMonth(int year, int month, Guid? userId)
        {
            var query = _context.Transactions
            .Where(w => w.CreateDate >= new DateTime(year, month, 1) && w.CreateDate <= new DateTime(year, month, DateTime.DaysInMonth(year, month)))
            .AsQueryable();

            if (userId != null)
                query = query.Where(w => w.UserId == userId);

            object transaction = new
            {
                TotalProcessing = query.Where(w => w.Status == 0).Count(),
                TotalTopup = query.Where(w => w.Status == 1 && w.PaymentType == 1).Count(),
                TotalMoneyTopup = query.Where(w => w.Status == 1 && w.PaymentType == 1).Sum(s => s.Money),
                TotalWithdraw = query.Where(w => w.Status == 1 && w.PaymentType == 0).Count(),
                TotalMoneyWithdraw = query.Where(w => w.Status == 1 && w.PaymentType == 0).Sum(s => s.Money),
                TotalDenied = query.Where(w => w.Status == -1 && w.PaymentType == 0).Count(),
                TotalMoneyDenied = query.Where(w => w.Status == -1 && w.PaymentType == 0).Sum(s => s.Money),
            };

            return transaction;
        }
    }
}