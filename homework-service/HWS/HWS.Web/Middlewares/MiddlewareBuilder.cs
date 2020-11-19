using HWS.Middlewares.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Middlewares
{
    public static class MiddlewareBuilder
    {
        public static void UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
        }

        public static void AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            configureApp(services, config);
        }

        private static void configureApp(IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(config.GetSection(nameof(AppSettings)));

            services.AddSingleton<IAppSettings>(sp =>
                sp.GetRequiredService<IOptions<AppSettings>>().Value);
        }
    }
}
