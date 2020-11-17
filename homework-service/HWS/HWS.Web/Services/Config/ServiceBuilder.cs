using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Services.Config
{
    public static class ServiceBuilder
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IHomeworkService, HomeworkService>();
        }
    }
}
