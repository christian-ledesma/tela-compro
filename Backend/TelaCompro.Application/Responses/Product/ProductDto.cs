using TelaCompro.Domain.Entities;

namespace TelaCompro.Application.Responses.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }
        public string? Owner { get; set; }
        public string? Buyer { get; set; }
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }
}
