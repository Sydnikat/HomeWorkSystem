using homework_service.Dal.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace homework_service.Dal.Config
{
    public static class MongoBuilder
    {
        public static void AddMongodbSettings(this IServiceCollection services)
        {
            services.AddTransient<IUserRepoitory, UserRepository>();
        }
    }
}
