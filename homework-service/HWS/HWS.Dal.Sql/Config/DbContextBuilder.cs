using HWS.Dal.Sql.Assignments;
using HWS.Dal.Sql.Groups;
using HWS.Dal.Sql.Homeworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Config
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

            services.AddTransient<IAssignmentRepository, AssignmentRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IHomeworkRepository, HomeworkRepository>();
        }

        private static void configureSqlServer(IServiceCollection services, IConfiguration config)
        {
            services.Configure<HWSDatabaseSettings>(config.GetSection(nameof(HWSDatabaseSettings)));

            services.AddSingleton<IHWSDatabaseSettings>(sp => sp.GetRequiredService<IOptions<HWSDatabaseSettings>>().Value);
        }
    }
}
