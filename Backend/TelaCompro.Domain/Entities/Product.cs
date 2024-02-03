namespace TelaCompro.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }
        public User? Owner { get; set; }
        public User? Buyer { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public IEnumerable<Tag>? Tags { get; set; }
    }
}
