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
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IPhoneImageRepository, PhoneImageRepository>();
            return services;
        }
        public static IServiceCollection ServiceInject(this IServiceCollection services)
        {
            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IPhoneImageService, PhoneImageService>();
            return services;
        }
        public static IServiceCollection ConverterInject(this IServiceCollection services)
        {
            services.AddScoped<IPhoneConverter, PhoneConverter>();
            services.AddScoped<IPhoneImageConverter, PhoneImageConverter>();
            services.AddScoped<IBrandConverter, BrandConverter>();
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
