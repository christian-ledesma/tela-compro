using TelaCompro.Application.Common;
using TelaCompro.Application.Requests.Product;
using TelaCompro.Domain.Entities;

namespace TelaCompro.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<Product>>> ListProducts();
        Task<Result> UploadProduct(UploadProductDto request);
        Task<Result> UpdateProduct(CreateProductDto request);
        Task<Result> DeleteProduct(int id);
        Task<Result> BuyProduct(BuyProductDto request);
    }
}
