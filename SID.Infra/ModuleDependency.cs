using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SID.Infra.Configurations;

namespace SID.Infra
{
    public static class ModuleDependency
    {

        public static void AddDatabaseModule(this IServiceCollection services)
        {
            services
                .AddEntityFrameworkMySql()
                .AddDbContext<MySqlContext>(
                    options => options.UseMySql(ServerVersion.AutoDetect("Server=localhost;DataBase=typedids;Uid=mysqlusr;Pwd=password;")
                )
            );
        }
    }
}