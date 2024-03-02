using TelaCompro.Application.Common;
using TelaCompro.Application.Requests.Product;
using TelaCompro.Application.Responses.Product;
using TelaCompro.Application.Services.Interfaces;
using TelaCompro.Domain.Entities;
using TelaCompro.Domain.Repositories;

namespace TelaCompro.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<User> _userRepository;

        public ProductService(IProductRepository productRepository,
            IRepository<Brand> brandRepository,
            IRepository<Category> categoryRepository,
            IRepository<Tag> tagRepository,
            IRepository<User> userRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Result> BuyProduct(int productId, BuyProductDto request)
        {
            try
            {
                var product = await _productRepository.Get(productId);
                var buyer = await _userRepository.Get(request.BuyerId);

                if (product is null)
                {
                    return Result.Failure("Producto no encontrado");
                }

                if (buyer is null)
                {
                    return Result.Failure("Comprador no encontrado");
                }

                if (buyer.Credits < product.Price)
                {
                    return Result.Failure("No dispone de saldo suficiente para comprar este producto");
                }

                buyer.Credits -= product.Price;
                product.Buyer = buyer;

                await _productRepository.Update(product);
                await _userRepository.Update(buyer);

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

        public async Task<Result<ProductDto>> GetProduct(int id)
        {
            try
            {
                var p = await _productRepository.Get(id);

                if (p is null)
                {
                    return Result<ProductDto>.Failure("Producto no encontrado");
                }

                var product = new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Size = p.Size,
                    Price = p.Price,
                    Image = p.Image,
                    Owner = p.Owner?.Name,
                    Buyer = p.Buyer?.Name,
                    Category = p.Category?.Name,
                    Brand = p.Brand?.Name,
                    Tags = p.Tags?.Select(x => x.Name)!
                };

                return Result<ProductDto>.Success(product);
            }
            catch (Exception ex)
            {
                return Result<ProductDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<IEnumerable<ProductDto>>> ListProducts()
        {
            try
            {
                var query = await _productRepository.GetQueryable();
                var products = query.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Size = x.Size,
                    Price = x.Price,
                    Image = x.Image,
                    Owner = x.Owner != null ? x.Owner.Name : null,
                    Buyer = x.Buyer != null ? x.Buyer.Name : null,
                    Category = x.Category != null ? x.Category.Name : null,
                    Brand = x.Brand != null ? x.Brand.Name : null,
                    Tags = x.Tags!.Select(x => x.Name)!
                }).ToList();
                return Result<IEnumerable<ProductDto>>.Success(products);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ProductDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result> UpdateProduct(UpdateProductDto request)
        {
            try
            {
                var product = await _productRepository.Get(request.Id);
                if (product is null)
                {
                    return Result.Failure("Producto no encontrado");
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
                    Owner = await _userRepository.Get(request.OwnerId),
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
