﻿namespace TelaCompro.Application.Requests.Product
{
    public class UploadProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<int> TagsId { get; set; }
    }
}
