namespace TelaCompro.Application.Services.Interfaces
{
    public interface IProductServices
    {
        Task ListProducts();
        Task UploadProduct();
        Task UpdateProduct();
        Task DeleteProduct();
        Task BuyProduct();
    }
}
