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

        public static void Seed(StoreContext context)
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

            var users = new List<User>
            {
                new()
                {
                    Name = "Christian",
                    FirstLastName = "Ledesma",
                    SecondLastName = "Aguilar",
                    Email = "christian.ledesma@telacompro.com",
                    PhoneNumber = "+34 675517349"
                },
                new()
                {
                    Name = "Omar",
                    FirstLastName = "Ledesma",
                    SecondLastName = "Aguilar",
                    Email = "omar.ledesma@telacompro.com",
                    PhoneNumber = "+34 675117609"
                }
            };

            context.Users.AddRange(users);

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

            var products = new List<Product>
            {
                new()
                {
                    Name = "Camiseta Hollister",
                    Description = "Se vende camiseta original",
                    Size = "M",
                    Price = 15.99M,
                    Owner = users.First(x => x.Name == "Christian"),
                    Category = categories.First(x => x.Name == "Shirts"),
                    Brand = brands.First(x => x.Name == "Hollister"),
                    Tags = tags
                }
            };

            context.Products.AddRange(products);

            context.SaveChanges();
        }
    }
}
