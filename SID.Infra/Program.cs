using Microsoft.Extensions.Hosting;
using SID.Infra;

string connString = "server=localhost;port=3306;userid=mysqlusr;password=password;database=typedids;";

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services.AddDatabaseModule(connString))
    .Build();

await host.RunAsync();