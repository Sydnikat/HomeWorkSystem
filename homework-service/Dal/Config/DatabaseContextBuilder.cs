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
    public static class DatabaseContextBuilder
    {
        public static void ConfigureDatabaseSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<UserDatabaseSettings>(config.GetSection(nameof(UserDatabaseSettings)));

            services.AddSingleton<IUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);


            services.Configure<HWSDatabaseSettings>(config.GetSection(nameof(HWSDatabaseSettings)));

            services.AddSingleton<IHWSDatabaseSettings>(sp => sp.GetRequiredService<IOptions<HWSDatabaseSettings>>().Value);
        }
    }
}
