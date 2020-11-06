using homework_service.Dal.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace homework_service.Dal.Config
{
    public static class MongoBuilder
    {
        public static void AddMongodb(this IServiceCollection services, IConfiguration config)
        {
            configureMongodb(services, config);

            services.AddTransient<IUserRepoitory, UserRepository>();
        }

        private static void configureMongodb(IServiceCollection services, IConfiguration config)
        {
            services.Configure<HWSDatabaseSettings>(config.GetSection(nameof(HWSDatabaseSettings)));

            services.AddSingleton<IHWSDatabaseSettings>(sp => sp.GetRequiredService<IOptions<HWSDatabaseSettings>>().Value);
        }
    }
}
