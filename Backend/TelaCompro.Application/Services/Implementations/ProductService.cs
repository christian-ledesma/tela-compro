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
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;

        public ProductService(IRepository<Product> productRepository,
            IRepository<Brand> brandRepository,
            IRepository<Category> categoryRepository,
            IRepository<Tag> tagRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        public async Task<Result> BuyProduct(BuyProductDto request)
        {
            try
            {
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.Delete(id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<Product>>> ListProducts()
        {
            try
            {
                var products = await _productRepository.GetAll();
                return Result<IEnumerable<Product>>.Success(products);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Product>>.Failure(ex.Message);
            }
        }

        public async Task<Result> UpdateProduct(UpdateProductDto request)
        {
            try
            {
                var product = await _productRepository.Get(request.Id);
                if (product is null)
                {
                    throw new Exception("Producto no encontradod");
                }

                var tagList = await _tagRepository.GetQueryable();

                product.Name = request.Name;
                product.Description = request.Description;
                product.Size = request.Size;
                product.Price = request.Price;
                product.Category = await _categoryRepository.Get(request.CategoryId);
                product.Brand = await _brandRepository.Get(request.BrandId);
                product.Tags = tagList.Where(x => request.TagsId.Contains(x.Id)).ToList();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> UploadProduct(UploadProductDto request)
        {
            try
            {
                var tagList = await _tagRepository.GetQueryable();
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Size = request.Size,
                    Price = request.Price,
                    Category = await _categoryRepository.Get(request.CategoryId),
                    Brand = await _brandRepository.Get(request.BrandId),
                    Tags = tagList.Where(x => request.TagsId.Contains(x.Id)).ToList()
                };

                await _productRepository.Create(product);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
