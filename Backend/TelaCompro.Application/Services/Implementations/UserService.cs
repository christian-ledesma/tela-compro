using TelaCompro.Application.Extensions;
using TelaCompro.Application.Requests.User;
using TelaCompro.Application.Responses;
using TelaCompro.Application.Services.Interfaces;
using TelaCompro.Domain.Entities;
using TelaCompro.Domain.Repositories;

namespace TelaCompro.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Result> Login(LoginDto request)
        {
            try
            {
                var query = await _userRepository.GetQueryable();
                var user = query.FirstOrDefault(x => x.Email == request.Email) ?? throw new Exception("Usuario no encontrado");

                if (request.Password is null)
                {
                    throw new Exception("Proporcionar contraseña");
                }

                var password = request.Password.Hash();
                if (user.Password != password)
                {
                    throw new Exception("Contraseña incorrecta");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> Register(RegisterDto request)
        {
            try
            {
                var password = request.Password?.Hash();
                var user = new User
                {
                    Name = request.Name,
                    FirstLastName = request.FirstLastname,
                    SecondLastName = request.SecondLastname,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Password = password
                };
                await _userRepository.Create(user);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
