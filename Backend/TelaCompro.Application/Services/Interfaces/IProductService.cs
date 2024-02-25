using TelaCompro.Application.Common;
using TelaCompro.Application.Requests.Product;
using TelaCompro.Application.Responses.Product;

namespace TelaCompro.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDto>>> ListProducts();
        Task<Result<ProductDto>> GetProduct(int id);
        Task<Result> UploadProduct(UploadProductDto request);
        Task<Result> UpdateProduct(UpdateProductDto request);
        Task<Result> DeleteProduct(int id);
        Task<Result> BuyProduct(int productId, BuyProductDto request);
    }
}
