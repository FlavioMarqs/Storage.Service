using Microsoft.Extensions.DependencyInjection;
using Storage.Commands.Commands;
using Storage.Handlers.Interfaces;
using Storage.Commands.Queries;
using Storage.Handlers.DTOs;

namespace Storage.Handlers
{
    public static class HandlersEx
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<StringStoreCommand, StringDTO>, StringCommandHandler>();
            services.AddTransient<ICommandHandler<StringQueryCommand, StringDTO>, StringQueryHandler>();
            services.AddTransient<ICommandHandler<StringsQueryCommand, IEnumerable<StringDTO>>, StringsQueryHandler>();

            return services;
        }
    }
}
