using System;
using System.Data;
using System.IO;
using System.Reflection;
using AspNetSandbox.Data;
using AspNetSandbox.Interfaces;
using AspNetSandbox.Services;
using CommandLine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace AspNetSandbox
{
    public class Program
    {
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }

            [Option('c', Required = false, HelpText = "Set connection string to connect to PostgresSql.")]
            public string ConnectToPostgresDatabase { get; set; }

            [Option('h', "help", Required = false, HelpText = "Help menu.")]
            public bool Help { get; set; }
        }

        public static int Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.Verbose)
                       {
                           Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
                           Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                       }
                       else
                       {
                           Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                           Console.WriteLine("Quick Start Example!");
                       }

                       if (o.Help)
                       {
                           //Console.WriteLine($"{o.Verbose.ToString()} {o.Verbose.}.");
                           //Console.WriteLine("c  ConnectionString for database connection.");
                           //Console.WriteLine("h  Help menu.");
                       }
                       if (o.ConnectToPostgresDatabase != "")
                       {
                           //var host = CreateHostBuilder(args).Build();

                           //var config = host.Services.GetRequiredService<IConfiguration>();
                           //var services = host.Services.GetRequiredService<IServiceCollection>();

                           ////Console.WriteLine(config.GetConnectionString($"{o.ConnectToPostgresDatabase}"));
                           //services.AddDbContext<ApplicationDbContext>(options =>
                           // options.UseNpgsql(config.GetConnectionString($"{o.ConnectToPostgresDatabase}")));

                           //host.Run();
                           //TestConnectionString(o.ConnectToPostgresDatabase);
                       }
                   });
            if (args.Length != 0)
            {
                System.Console.WriteLine($"There are {args.Length} arguments.");
            }

            CreateHostBuilder(args).Build().Run();
            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        static void TestConnectionString(string connectionString)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
                Console.WriteLine("Success open postgreSQL connection.");
            conn.Close();
        }
    }
}
