using TelaCompro.Application.Common;
using TelaCompro.Application.Requests.Product;
using TelaCompro.Application.Services.Interfaces;
using TelaCompro.Domain.Entities;
using TelaCompro.Domain.Repositories;

namespace TelaCompro.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public Task<Result> BuyProduct(BuyProductDto request)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Product>>> ListProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateProduct(CreateProductDto request)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UploadProduct(UploadProductDto request)
        {
            throw new NotImplementedException();
        }
    }
}
