using phone_shop_server.Business.Converter;
using phone_shop_server.Business.Service;
using phone_shop_server.Database.Repository;
using phone_shop_server.Util;

namespace phone_shop_server.Extension.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection RepositoryInject(this IServiceCollection services)
        {   
            services.AddScoped<IUserRepository, UserRepository>();   
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IHomeletRepository, HomeletRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IPhoneImageRepository, PhoneImageRepository>();
            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<ITicketDetailRepository, TicketDetailRepository>();
            services.AddScoped<IWarehouseTicketRepository, WarehouseTicketRepository>();
            return services;
        }
        public static IServiceCollection ServiceInject(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISupplierService, SupplierService>();    
            services.AddScoped<IPhoneImageService, PhoneImageService>();
            services.AddScoped<IWarehouseTicketService, WarehouseTicketService>();
            return services;
        }
        public static IServiceCollection ConverterInject(this IServiceCollection services)
        {
            services.AddScoped<IUserConverter, UserConverter>();
            services.AddScoped<ICartConverter, CartConverter>();
            services.AddScoped<IPhoneConverter, PhoneConverter>();
            services.AddScoped<IBrandConverter, BrandConverter>();
            services.AddScoped<IOrderConverter, OrderConverter>();
            services.AddScoped<IAddressConverter, AddressConverter>();
            services.AddScoped<ISupplierConverter, SupplierConverter>();
            services.AddScoped<IPhoneImageConverter, PhoneImageConverter>();
            services.AddScoped<IWarehouseTicketConverter, WarehouseTicketConverter>();
            return services;
        }
        public static IServiceCollection UtilInject(this IServiceCollection services) 
        {
            services.AddScoped<IJwtUtil, JwtUtil>();
            services.AddScoped<IMailUtil, MailUtil>();
            services.AddScoped<IFileUtil, FileUtil>();
            return services;
        }
    }
}
