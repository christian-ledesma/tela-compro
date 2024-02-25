using Microsoft.EntityFrameworkCore;
using TelaCompro.Domain.Entities;
using TelaCompro.Domain.Repositories;
using TelaCompro.Infrastructure.Persistence;

namespace TelaCompro.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Product?> Get(int id)
        {
            var product = await _context.Products
                                    .Where(x => x.Id == id)
                                    .Include(x => x.Owner)
                                    .Include(x => x.Buyer)
                                    .Include(x => x.Category)
                                    .Include(x => x.Brand)
                                    .Include(x => x.Tags)
                                    .FirstOrDefaultAsync();
            return product;
        }
    }
}
