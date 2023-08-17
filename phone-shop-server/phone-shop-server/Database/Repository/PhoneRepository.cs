using Microsoft.EntityFrameworkCore;
using phone_shop_server.Business.Service;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Enum;

namespace phone_shop_server.Database.Repository
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IReportRepository _reportRepository;
        public PhoneRepository(
            AppDbContext appDbContext, 
            IReportRepository reportRepository
            )
        {
            _appDbContext = appDbContext;
            _reportRepository = reportRepository;
        }
        public async Task<IEnumerable<Phone>> GetAllAsync()
        {
            return await _appDbContext.Phone.ToListAsync();
        }
        public async Task<Phone> GetOneAsync(string id) 
        {
            return await _appDbContext.Phone.FirstOrDefaultAsync(p => p.Id.ToString().Equals(id));
        }
        public async Task<Phone> CreateAsync(Phone phone)
        {
            _appDbContext.Phone.Add(phone);
            await _appDbContext.SaveChangesAsync();
            return phone;
        }
        public async Task<Phone> UpdateAsync(Phone phone)
        {
            _appDbContext.Update(phone);
            await _appDbContext.SaveChangesAsync();
            return phone;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            Phone phone = await GetOneAsync(id);
            _appDbContext.Phone.Remove(phone);
            var x = await _appDbContext.SaveChangesAsync();
            return x > 0;
        }
        public PhoneImage getPhoneImage(string id)
        {
            return _appDbContext.PhoneImage.Where(p => p.PhoneId.ToString() == id).ToList().ElementAt(0);
        }
        public async Task<IEnumerable<object>> GetBestSeller(int count)
        {
            string canceledId = (_appDbContext.Status.FirstOrDefault(s => s.StatusType == "CANCELED")).Id.ToString();
            var orderNotLegit = await _appDbContext.OrderStatus
                .Where(o => o.StatusId.Equals(canceledId)).Distinct().Select(o => o.OrderId.ToString()).ToListAsync();
            var phone = await _appDbContext.OrderDetail.Where(o => !orderNotLegit.Contains(o.Id.ToString()))
                .GroupBy(o => o.PhoneId).Select(o => new { phoneId = o.Key, quantity = o.Sum(o => o.Quantity) })
                .OrderByDescending(o => o.quantity).ToListAsync();
            var result = (from p in _appDbContext.Phone.Include(p => p.PhoneImages).AsEnumerable()
                              join best in phone.AsEnumerable()
                             on p.Id equals best.phoneId
                              select new
                              {
                                  phoneId = p.Id,
                                  quantity = p.Quantity,
                                  name = p.Name,
                                  image = p.PhoneImages.AsEnumerable().ElementAt(0).Link
                              }).ToList();
            return result.Take(count);
        }
        public async Task<IEnumerable<object>> GetAllRole()
        {
            return await _appDbContext.Roles.Select(r =>
            new
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
        }
        public async Task<object> AdminDashBoard()
        {
            var bestSeller = await GetBestSeller(5);
            string canceledId = (_appDbContext.Status.FirstOrDefault(s => s.StatusType == "CANCELED")).Id.ToString();
            var canceledOrder = await _appDbContext.OrderStatus
                .Where(o => o.StatusId.Equals(canceledId)).Distinct().Select(o => o.OrderId.ToString()).ToListAsync();
            //
            var orders = await _appDbContext.Order.Where(o => !canceledOrder.Contains(o.Id.ToString())).ToListAsync();
            //
            double totalPrice = 0;
            var listOrderDetail = await _appDbContext.OrderDetail
                .Include(o => o.Phone)
                .Where(o => !canceledOrder.Contains(o.OrderId.ToString())).
                Select(o => o.Quantity * o.Phone.SoldPrice).ToListAsync();
            listOrderDetail.ForEach(o => totalPrice += o);
            var customerRole = await _appDbContext.Roles.Where(r => r.Name == DbUserRole.CUSTOMER.ToString()).FirstOrDefaultAsync();
            var users =await (from u in _appDbContext.Users
                         join ur in _appDbContext.UserRoles
                        on u.Id equals ur.UserId
                         where ur.RoleId == customerRole.Id
                         select u).ToListAsync();
            var todayStat = await _reportRepository.getStatByDATE(DateTime.Now.ToString("MM.dd.yyyy"));
            var phones = await _appDbContext.Phone.OrderBy(p => p.BrandId).Select(p => new { }).ToListAsync();
            return new 
            {
                bestSeller = bestSeller,
                totalOrder = orders.Count,
                totalPrice = totalPrice,
                users = users,
                todayStat = todayStat,
                allOrders = orders.Select(async o => new 
                {
                    Id = o.Id,
                    Create = o.CreateDate,
                    UserId = o.UserId,
                    TotalPrice = 0
                    
                }).ToList(),
            };
        }
        public async Task<double> TotalOrderMoney(string orderId)
        {
            double total = 0;
            var list =await _appDbContext.OrderDetail.Where(o => o.OrderId.ToString() == orderId).Select(o => o.Quantity*o.Phone.SoldPrice).ToListAsync();
            list.ForEach(o => total += o);
            return total;
        }
    }
}
