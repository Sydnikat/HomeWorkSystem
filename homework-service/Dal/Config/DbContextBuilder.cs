using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_service.Dal.Config
{
    public static class DbContextBuilder
    {
        public static void AddSqlServer(this IServiceCollection services, IConfiguration config)
        {
            configureSqlServer(services, config);

            var sp = services.BuildServiceProvider();
            var settings = sp.GetService<IHWSDatabaseSettings>();

            services.AddDbContext<HWSContext>(o =>
            {
                o.UseSqlServer(settings.MSSQLConnection);
            });
        }

        private static void configureSqlServer(IServiceCollection services, IConfiguration config)
        {
            services.Configure<UserDatabaseSettings>(config.GetSection(nameof(UserDatabaseSettings)));

            services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);
        }
    }
}
