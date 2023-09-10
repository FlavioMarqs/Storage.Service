using Storage.Repositories;
using Storage.Handlers;
using Microsoft.AspNetCore.Builder;

namespace Storage.Service
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRepositories(_config);
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddHandlers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseExceptionHandler();
        }
    }
}
