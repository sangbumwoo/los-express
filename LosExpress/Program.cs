using System.Diagnostics.CodeAnalysis;
using Btc.Web.Logger;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System.Net;
using Microsoft.Extensions.Hosting;

namespace LosExpress
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = 10;
            CreateHostBuilder(args).Build().Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                            .ReadFrom.Configuration(hostingContext.Configuration)
                            .Enrich.FromLogContext()
                            .WriteTo.BtcUdp(
                                "monitor-fluentd.service.consul",
                                41234,
                                new StandardLogJsonFormatter())
                        );
                });
    }
}