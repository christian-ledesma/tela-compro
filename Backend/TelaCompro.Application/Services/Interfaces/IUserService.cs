using TelaCompro.Application.Requests.User;
using TelaCompro.Application.Responses;

namespace TelaCompro.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> Register(RegisterDto request);
        Task<Result> Login(LoginDto request);
        Task<Result> UpdateUser(UpdateUserDto request);
    }
}
