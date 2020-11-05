using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_service.Dal.Config
{
    public static class DbContextBuilder
    {
        public static void AddDbContexts(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var settings = sp.GetService<IHWSDatabaseSettings>();

            services.AddDbContext<HWSContext>(o =>
            {
                o.UseSqlServer(settings.MSSQLConnection);
            });
        }
    }
}
