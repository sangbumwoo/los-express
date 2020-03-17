using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using LosExpress.Utils;
using Btc.Web.Filters;
using Btc.Swagger;
using Btc.Web.Utils;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting;

namespace LosExpress
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddBtcWeb(Configuration)
                .AddServices(Configuration)
                .AddBtcInfoEndpoint(Configuration)
                .AddApplicationInsightsTelemetry(Configuration)
                .AddHsts(options => { options.Preload = true; })
                .AddBtcSwagger(typeof(Startup).GetTypeInfo().Assembly)
                .AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>())
                .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest)
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers(o => { o.Filters.Add(typeof(LogRouteTemplateFilter)); })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddHttpClient("GitHub", client => {
                client.BaseAddress = new System.Uri("https://api.github.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryExample");
            });

            // Add health check dependencies and checks
            services
                .AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseBtcWeb();
            app.UseRouting();
            app.UseResponseCompression();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBtcHealth();
                endpoints.MapBtcInfo();
                endpoints.MapBtcHealthUpstreams();
            });
            app.UseBtcSwagger(provider);
        }
    }
}
