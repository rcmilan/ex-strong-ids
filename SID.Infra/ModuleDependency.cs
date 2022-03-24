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
                .AddDbContext<MySqlContext>(
                    options => options.UseMySql(ServerVersion.AutoDetect("server=mysql;port=3306;userid=mysqlusr;password=password;database=typedids;"))
                );
        }
    }
}