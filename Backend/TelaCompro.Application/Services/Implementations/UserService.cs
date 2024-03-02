using TelaCompro.Application.Common;
using TelaCompro.Application.Extensions;
using TelaCompro.Application.Requests.User;
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

        public async Task<Result> AddCredits(AddUserCreditsDto request)
        {
            try
            {
                var user = await _userRepository.Get(request.UserId);

                if (user is null)
                {
                    return Result.Failure("Usuario no encontrado");
                }

                user.Credits += request.Credits;
                await _userRepository.Update(user);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> Login(LoginDto request)
        {
            try
            {
                var query = await _userRepository.GetQueryable();
                var user = query.FirstOrDefault(x => x.Email == request.Email) ?? throw new Exception("Usuario no encontrado");

                if (request.Password is null)
                {
                    return Result.Failure("Proporcionar contraseña");
                }

                var password = request.Password.Hash();
                if (user.Password != password)
                {
                    return Result.Failure("Contraseña incorrecta");
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

        public async Task<Result> UpdateUser(UpdateUserDto request)
        {
            try
            {
                var user = await _userRepository.Get(request.Id);
                if (user is null)
                {
                    return Result.Failure("Usuario no encontrado");
                }

                user.Name = request.Name;
                user.FirstLastName = request.FirstLastName;
                user.SecondLastName = request.SecondLastName;
                user.PhoneNumber = request.PhoneNumber;
                user.Email = request.Email;

                await _userRepository.Update(user);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
