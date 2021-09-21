using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AspNetSandbox.Data;
using AspNetSandbox.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace AspNetSandbox
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(GetConnectionString()));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSignalR();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddRazorPages();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiSendbox", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
            services.AddSignalR();
            services.AddScoped<IBookRepository, DbBooksRepository>();
        }

        private string GetConnectionString()
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (connectionString != null)
            {
                return ConvertConnectionString(connectionString);
            }

            //Replace Default Connection with ConvertConnectionString(connectionString)
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public static string ConvertConnectionString(string connectionString)
        {
            Uri uri = new (connectionString);

            return $"Database={uri.AbsolutePath.Split("/")[1]}; Host={uri.Host}; Port={uri.Port}; User Id={uri.UserInfo.Split(":")[0]}; Password={uri.UserInfo.Split(":")[1]}; SSL Mode=Require;Trust Server Certificate=true";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiSendbox v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            var defaultFilesOptions = new DefaultFilesOptions();
            app.UseDefaultFiles(defaultFilesOptions);
            defaultFilesOptions.DefaultFileNames = new List<string>();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/messagehub");
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            app.SeedData();
        }
    }
}
