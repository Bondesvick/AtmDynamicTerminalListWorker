using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using AtmDynamicTerminalListWorker.Entities.Finacle;
using AtmDynamicTerminalListWorker.Entities.Post;
using AtmDynamicTerminalListWorker.Entities.Rpa;
using AtmDynamicTerminalListWorker.Interfaces;
using AtmDynamicTerminalListWorker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AtmDynamicTerminalListWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggerFactory => loggerFactory.AddEventLog().AddConsole())
                .UseNLog()
                .UseWindowsService()
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    //services.AddSingleton<IPostDataService, PostDataServiceSp>();
                    services.AddSingleton<IPostDataService, PostDataServiceQuery>();
                    // services.AddSingleton<IFinacleDataService, FinacleDataServiceSp>();
                    services.AddSingleton<IFinacleDataService, FinacleDataServiceQuery>();
                    services.AddSingleton<IRpaDataService, RpaDataService>();
                    services.AddDbContext<RpaDbContext>(
                        options => options.UseSqlServer(
                            hostBuilderContext.Configuration.GetConnectionString("RpaDBConnection")),
                        ServiceLifetime.Singleton,
                        ServiceLifetime.Singleton);
                    services.AddDbContext<PostDbContext>(
                        options => options.UseSqlServer(
                            hostBuilderContext.Configuration.GetConnectionString("PostDBConnection")),
                        ServiceLifetime.Singleton,
                        ServiceLifetime.Singleton);
                    services.AddDbContext<FinacleDbContext>(
                        options => options.UseOracle(
                            hostBuilderContext.Configuration.GetConnectionString("FinacleDBConnection")),
                        ServiceLifetime.Singleton,
                        ServiceLifetime.Singleton);
                    services.AddHostedService<Worker>();
                });
    }
}