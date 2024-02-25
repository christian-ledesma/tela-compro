using Microsoft.AspNetCore.Mvc;
using TelaCompro.Application.Common;
using TelaCompro.Application.Requests.Product;
using TelaCompro.Application.Responses.Product;
using TelaCompro.Application.Services.Interfaces;

namespace TelaCompro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<Result<IEnumerable<ProductDto>>> List()
        {
            var response = await _productService.ListProducts();
            return response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Result<ProductDto>> Get(int id)
        {
            var response = await _productService.GetProduct(id);
            return response;
        }

        [HttpPost]
        public async Task<Result> Upload(UploadProductDto request)
        {
            var response = await _productService.UploadProduct(request);
            return response;
        }

        [HttpPut]
        public async Task<Result> Update(UpdateProductDto request)
        {
            var response = await _productService.UpdateProduct(request);
            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<Result> Delete(int id)
        {
            var response = await _productService.DeleteProduct(id);
            return response;
        }

        [HttpPost]
        [Route("{id}/[action]")]
        public async Task<Result> Buy(int id, BuyProductDto request)
        {
            var response = await _productService.BuyProduct(id, request);
            return response;
        }
    }
}
