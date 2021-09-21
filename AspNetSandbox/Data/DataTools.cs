using System;
using System.Linq;
using AspNetSandbox.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
                    Book book1 = new ()
                    {
                        BookTitle = "Arta manipularii",
                        BookAuthor = "Kevin Dutton",
                        BookLanguage = "Romanian",
                    };
                    Book book2 = new ()
                    {
                        BookTitle = "Puterea prezentului",
                        BookAuthor = "Eckhart Tolle",
                        BookLanguage = "Romanian",
                    };
                    appDbContext.Add(book1);
                    appDbContext.Add(book2);
                    appDbContext.SaveChanges();
                }
            }
        }
    }
}
