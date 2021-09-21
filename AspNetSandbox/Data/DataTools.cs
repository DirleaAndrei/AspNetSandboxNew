using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetSandbox.Data
{
    public static class DataTools
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var appDbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (appDbContext.Books.Any())
                {
                    Console.WriteLine("The books are there!");
                }
                else
                {
                    Console.WriteLine("There are no books!");
                }
            }
        }
    }
}
