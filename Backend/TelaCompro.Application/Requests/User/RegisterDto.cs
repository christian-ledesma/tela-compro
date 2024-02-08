namespace TelaCompro.Application.Requests.User
{
    public class RegisterDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? FirstLastname { get; set; }
        public string? SecondLastname { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
