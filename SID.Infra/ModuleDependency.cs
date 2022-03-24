using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using SID.Infra.Configurations;

namespace SID.Infra
{
    public static class ModuleDependency
    {

        public static void AddDatabaseModule(this IServiceCollection services)
        {
            var connectionString = "server=localhost;port=3306;userid=mysqlusr;password=password;database=typedids;";
            var mySqlConnection = new MySqlConnection(connectionString);
            
            services
                .AddDbContext<MySqlContext>(
                    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(mySqlConnection))
                );
        }
    }
}