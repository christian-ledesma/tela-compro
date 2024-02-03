namespace TelaCompro.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
