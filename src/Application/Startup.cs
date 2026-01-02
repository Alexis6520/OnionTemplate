using Application.Features.Donuts;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Startup
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices()
        {
            services.AddTransient<DonutsService>();
            return services;
        }
    }
}
