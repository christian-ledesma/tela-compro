using TelaCompro.Application.Common;
using TelaCompro.Application.Requests.User;

namespace TelaCompro.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> Register(RegisterDto request);
        Task<Result> Login(LoginDto request);
        Task<Result> UpdateUser(UpdateUserDto request);
    }
}
