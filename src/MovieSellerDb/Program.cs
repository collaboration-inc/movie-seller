using DbUp;
using Microsoft.Extensions.Configuration;
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

    //        Console.WriteLine("Hello World!");
    //        var connectionString =
    //args.FirstOrDefault()
    //?? @"Server=localhost\MSSQLSERVER01;Database=movieSeller;Trusted_Connection=True;";
    //        EnsureDatabase.For.SqlDatabase(connectionString);

            var connectionString = Configuration.GetConnectionString("DevDatabase");

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

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
            return 0;
        }
    }
}
