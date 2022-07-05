using HotelApp.Api.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Serilog;

namespace HotelApp.API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Log.Information("Aplication Started");
            var host = CreateHostBuilder(args).Build();


            //using (var scope = host.Services.CreateScope())
            //{
            //    try
            //    {
            //        var context = scope.ServiceProvider.GetService<HotelDbContext>();
            //        context.Database.Migrate();
            //        Log.Information("Database migrated!");
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred while migrating the database.");
            //    }
            //}

            host.Run();
        }

        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog((hostingContext, LoggerConfiguration) =>
            {
                LoggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
            });

    }
}
