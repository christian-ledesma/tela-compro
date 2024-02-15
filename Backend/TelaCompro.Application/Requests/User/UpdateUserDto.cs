namespace TelaCompro.Application.Requests.User
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FirstLastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
