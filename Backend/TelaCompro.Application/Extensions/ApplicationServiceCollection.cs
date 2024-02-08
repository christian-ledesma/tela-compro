using Microsoft.Extensions.DependencyInjection;
using TelaCompro.Application.Services.Implementations;
using TelaCompro.Application.Services.Interfaces;

namespace TelaCompro.Application.Extensions
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
