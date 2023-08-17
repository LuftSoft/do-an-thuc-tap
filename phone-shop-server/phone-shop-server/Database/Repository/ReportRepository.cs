using Microsoft.EntityFrameworkCore;
using phone_shop_server.Business.Converter;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Enum;

namespace phone_shop_server.Database.Repository
{
    public class DateStat
    {
        public string Date { get; set; }
        public int TotalOrder { get; set; }
        public int TotalProduct { get; set; }
        public double TotalPrice { get; set; }
    }
    public class MonthStat
    {
        public int Month { get; set; }
        public int TotalOrder { get; set; }
        public int TotalProduct { get; set; }
        public double TotalPrice { get; set; }
    }
    public interface IReportRepository
    {
        Task<DateStat> getStatByDATE(string date);
        Task<MonthStat> getStatByMONTH(int month, int year);
        Task<IEnumerable<MonthStat>> MONTHStat(int year);
        Task<IEnumerable<DateStat>> getStatByConfig(string config);
        Task<IEnumerable<DateStat>> getStatByDateRange(string start, string end);

    }
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _appDbContext;
        public ReportRepository(
            AppDbContext appDbContext
            )
        {
            _appDbContext = appDbContext;
        }
        public async Task<DateStat> getStatByDATE(string date)
        {
            int total_product = 0;
            int total_order = 0;
            double total_money = 0;
            string canceledId = (_appDbContext.Status.FirstOrDefault(s => s.StatusType == "CANCELED")).Id.ToString();
            DateTime dateTime = DateTime.Parse(date);
            var canCeledorders = await (from o in _appDbContext.Order
                                join os in _appDbContext.OrderStatus on o.Id equals os.OrderId
                                where (o.CreateDate.DayOfYear == dateTime.DayOfYear && o.CreateDate.Year == dateTime.Year
                                    && os.StatusId.ToString().Equals(canceledId))
                                select o.Id.ToString()).Distinct().ToListAsync();
            var orders = await _appDbContext.Order.Where(o => o.CreateDate.DayOfYear == dateTime.DayOfYear && o.CreateDate.Year == dateTime.Year
                                   &&!canCeledorders.Contains(o.Id.ToString())).ToListAsync();
            total_order = orders.Count;
            var orderDetails = new List<OrderDetail>();
            foreach (var o in orders)
            {
                var tmp = await _appDbContext.OrderDetail.Where(od => od.OrderId.Equals(o.Id))
                    .Include(od => od.Phone).Select(od => od).ToListAsync();
                orderDetails.AddRange(tmp.AsEnumerable());
                
            };
            orderDetails.ForEach(orderDetail =>
            {
                total_product += orderDetail.Quantity;
                total_money += orderDetail.Quantity * orderDetail.Phone.SoldPrice;
            });
            return new DateStat()
            {
                Date = dateTime.ToString(),
                TotalOrder = total_order,
                TotalPrice = total_money,
                TotalProduct = total_product,
            };
        }
        public async Task<MonthStat> getStatByMONTH(int month, int year)
        {
            int total_product = 0;
            int total_order = 0;
            double total_money = 0;
            string canceledId = (await _appDbContext.Status.FirstOrDefaultAsync(s => s.StatusType == "CANCELED")).Id.ToString();
            var canCeledorders = await (from o in _appDbContext.Order
                                join os in _appDbContext.OrderStatus on o.Id equals os.OrderId
                                where (o.CreateDate.Month == month && o.CreateDate.Year == year
                                    && os.StatusId.ToString().Equals(canceledId))
                                select o.Id.ToString()).Distinct().ToListAsync();
            var orders = await _appDbContext.Order.Where(o => o.CreateDate.Month == month && o.CreateDate.Year == year
                                    && !canCeledorders.Contains(o.Id.ToString())).ToListAsync();
            total_order = orders.Count;
            var orderDetails = new List<OrderDetail>();
            foreach (var order in orders)
            {
                orderDetails.AddRange(await _appDbContext.OrderDetail.Where(o => o.OrderId.Equals(order.Id))
                .Include(o => o.Phone).ToListAsync());
            }
            orderDetails.ForEach(orderDetail =>
            {
                total_product += orderDetail.Quantity;
                total_money += (orderDetail.Quantity * orderDetail.Phone.SoldPrice);
            });
            return new MonthStat()
            {
                Month = month,
                TotalOrder = total_order,
                TotalPrice = total_money,
                TotalProduct = total_product,
            };
        }
        public async Task<IEnumerable<MonthStat>> MONTHStat(int year)
        {
            List<MonthStat> monthStats = new List<MonthStat>();
            for (int i = 1; i <= 12; i++)
            {
                monthStats.Add(await getStatByMONTH(i, year));
            }
            return monthStats;
        }
        public async Task<IEnumerable<DateStat>> getStatByConfig(string config)
        {
            List<DateStat> stat = new List<DateStat>();
            switch (config)
            {
                case "day":
                    var today = DateTime.Now;
                    for (var day = today.DayOfYear - 30; day <= today.DayOfYear; day = day + 1)
                    {
                        int year = DateTime.Now.Year; //Or any year you want
                        DateTime theDate = new DateTime(year, 1, 1).AddDays(day - 1);
                        string searchDay = theDate.ToString("MM.dd.yyyy");
                        stat.Add(await getStatByDATE(searchDay));
                    }
                    break;
                case "week":
                    break;
                case "month":
                    for (int i = 1; i <= 12; i++)
                    {

                    }
                    break;
            }
            return stat;
        }
        public async Task<IEnumerable<DateStat>> getStatByDateRange(string start, string end)
        {
            List<DateStat> stat = new List<DateStat>();
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            for(var date = startDate; date<=endDate; date = date.AddDays(1))
            {
                stat.Add(await getStatByDATE(date.ToLongDateString()));
            }
            return stat;

        }
    }
}

