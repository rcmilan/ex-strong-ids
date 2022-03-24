using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using SID.Domain.Entities;
using SID.Domain.Interfaces;
using SID.Infra.Configurations;
using SID.Infra.Repositories;

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

            services.AddScoped<IRepository<School, SchoolId>, Repository<School, SchoolId>>();
        }
    }
}