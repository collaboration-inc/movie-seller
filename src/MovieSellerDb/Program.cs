using DbUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;

namespace MovieSellerDb
{
    class Program
    {
        static int Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            InitDatabase(Configuration);

            return 0;
        }

        private static int InitDatabase(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DevDatabase");
            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .WithTransaction()
                    .LogToConsole()
                    .Build();

            if (upgrader.IsUpgradeRequired())
            {
                var result = upgrader.PerformUpgrade();
                if (!result.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.Error);
                    Console.ResetColor();
                    #if DEBUG
                    Console.ReadLine();
                    #endif
                    return -1;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();

            }
            return 0;
        }
    }
}
