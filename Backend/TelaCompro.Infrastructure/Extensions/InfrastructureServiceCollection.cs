using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelaCompro.Domain.Entities;
using TelaCompro.Domain.Repositories;
using TelaCompro.Infrastructure.Persistence;
using TelaCompro.Infrastructure.Repositories;

namespace TelaCompro.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Store"));
            });
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

        public static StoreContext Seed(this StoreContext context)
        {
            if (!context.Brands.Any())
            {
                var brands = new List<Brand>
                {
                    new()
                    {
                        Name = "Hollister",
                        IsLuxury = false
                    },
                    new()
                    {
                        Name = "Prada",
                        IsLuxury = true
                    },
                    new()
                    {
                        Name = "Bershka",
                        IsLuxury = false
                    }
                };

                context.Brands.AddRange(brands);
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new()
                    {
                        Name = "Shirts"
                    },
                    new()
                    {
                        Name = "Pants"
                    }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Tags.Any())
            {
                var tags = new List<Tag>
                {
                    new()
                    {
                        Name = "#Collection",
                    },
                    new()
                    {
                        Name = "#Fashion"
                    }
                };

                context.Tags.AddRange(tags);
                context.SaveChanges(); 
            }

            return context;
        }
    }
}
