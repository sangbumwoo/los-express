using System.Diagnostics.CodeAnalysis;
using LosExpress.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace LosExpress.Utils
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection, IConfiguration config)
        {
            // Add your service registrations here
            serviceCollection.AddTransient<IExampleUsersService, ExampleUsersService>();
            //serviceCollection.AddTransient<IPoiService, PoiService>();

            return serviceCollection;
        }
    }
}
