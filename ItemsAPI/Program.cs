using ItemsAPI.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;

namespace ItemsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder
                .ConfigureNLog("nlog.config")
                .GetCurrentClassLogger();

            //Catch error while bootstrapping
            try
            {
                logger.Info("Test error");
                var host = CreateWebHostBuilder(args).Build();

                using ( var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<ItemInfoContext>();

                        context.Database.EnsureDeleted();
                        context.Database.Migrate();
                    }
                    catch (Exception e)
                    {

                        logger.Error(e, "An error occured while migrating the database");
                    }
                }

                host.Run();

            }
            catch (Exception e)
            {
                logger.Error(e, "Application stopped because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>()
            .UseNLog();

    }
}
