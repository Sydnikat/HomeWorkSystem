using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HWS.Controllers.DTOs.Config;
using HWS.Dal.Mongo.Config;
using HWS.Dal.Sql.Config;
using HWS.Middlewares;
using HWS.Services.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HWS
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_allowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure DAL
            services.AddMongodb(this.Configuration);
            services.AddSqlServer(this.Configuration);

            services.AddAppConfiguration(this.Configuration);

            services.AddServices();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                {
                    builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddControllers()
                .SetupJsonConverters()
                .SetupJsonSerialization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddlewares();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}")
                .RequireCors(MyAllowSpecificOrigins);
            });
        }
    }
}
