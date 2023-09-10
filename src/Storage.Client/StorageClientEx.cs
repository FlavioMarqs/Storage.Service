

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Client.Interfaces;

namespace Storage.Client
{
    public static class StorageClientEx
    {
        public static IServiceCollection AddStorageClient(this IServiceCollection serviceProvider, IConfiguration configuration)
        {
            var options = new StorageClientOptions() { ApiServiceUrl = configuration["ApiServiceUrl"] };
            serviceProvider.AddSingleton<StorageClientOptions>(options);
            serviceProvider.AddTransient<IStorageStringsClient, StorageStringsClient>();

            return serviceProvider;
        }
    }
}
