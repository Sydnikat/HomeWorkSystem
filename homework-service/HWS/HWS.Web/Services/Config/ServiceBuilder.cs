using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Config
{
    public static class ServiceBuilder
    {
        public static void AddServices(this IServiceCollection services, IConfiguration config)
        {
            configureFileSettings(services, config);

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IHomeworkService, HomeworkService>();
            services.AddTransient<IAssignmentService, AssignmentService>();
        }

        private static void configureFileSettings(IServiceCollection services, IConfiguration config)
        {
            services.Configure<FileSettings>(config.GetSection(nameof(FileSettings)));

            services.AddSingleton<IFileSettings>(sp => sp.GetRequiredService<IOptions<FileSettings>>().Value);
        }
    }
}
