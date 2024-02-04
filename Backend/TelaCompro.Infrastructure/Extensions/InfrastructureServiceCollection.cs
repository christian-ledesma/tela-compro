using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelaCompro.Infrastructure.Data;

namespace TelaCompro.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StoreServer"));
            });
            return services;
        }
    }
}
