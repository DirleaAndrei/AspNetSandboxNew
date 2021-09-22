using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AspNetSandbox
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if(args.Length != 0)
            {
                System.Console.WriteLine($"There are {args.Length} arguments.");
                //foreach (arg in args)
                //{
                //    System.Console.WriteLine(arg);
                //}
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
    }
}
