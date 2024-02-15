using Moq;
using TelaCompro.Application.Extensions;
using TelaCompro.Application.Requests.User;
using TelaCompro.Application.Services.Implementations;
using TelaCompro.Domain.Entities;
using TelaCompro.Domain.Repositories;

namespace TelaCompro.Application.Tests
{
    public class UserServiceUnitTests
    {
        private readonly UserService _userService;
        private readonly Mock<IRepository<User>> _userRepository;

        public UserServiceUnitTests()
        {
            _userRepository = new Mock<IRepository<User>>();
            _userService = new UserService(_userRepository.Object);

            var users = new List<User>() { new() { Id = 1, Email = "test@email.com", Password = "123456".Hash() } };
            var query = users.AsQueryable();

            _userRepository.Setup(x => x.GetQueryable()).Returns(Task.FromResult(query));
        }

        [Fact]
        public async Task Login_WithUnexistingEmail_ShouldFail()
        {
            // Arrange
            var request = new LoginDto
            {
                Email = "test@mail.com",
                Password = "password"
            };

            // Act
            var response = await _userService.Login(request);

            // Asset
            Assert.False(response.IsSuccess);
            Assert.Equal("Usuario no encontrado", response.Error);
        }

        [Fact]
        public async Task Login_WithNoPassword_ShouldNotSucceed()
        {
            // Arrange
            var request = new LoginDto
            {
                Email = "test@email.com",
                Password = null
            };

            // Act
            var response = await _userService.Login(request);

            // Asset
            Assert.False(response.IsSuccess);
            Assert.Equal("Proporcionar contraseña", response.Error);
        }

        [Fact]
        public async Task Login_WithWrongPassword_ShouldNotSucceed()
        {
            // Arrange
            var request = new LoginDto
            {
                Email = "test@email.com",
                Password = "12345"
            };

            // Act
            var response = await _userService.Login(request);

            // Asset
            Assert.False(response.IsSuccess);
            Assert.Equal("Contraseña incorrecta", response.Error);
        }

        [Fact]
        public async Task Login_WithCorrectCredentials_ShouldSucceed()
        {
            // Arrange
            var request = new LoginDto
            {
                Email = "test@email.com",
                Password = "123456"
            };

            // Act
            var response = await _userService.Login(request);

            // Asset
            Assert.True(response.IsSuccess);
            Assert.Null(response.Error);
        }

        [Fact]
        public async Task Register_ShouldSucceed()
        {
            // Arrange
            var request = new RegisterDto
            {
                Name = "Test",
                FirstLastname = "FirstTest",
                SecondLastname = "LastTest",
                PhoneNumber = "+1 555555555",
                Email = "test@email.com",
                Password = "123456"
            };

            // Act
            var response = await _userService.Register(request);

            // Asset
            Assert.True(response.IsSuccess);
            Assert.Null(response.Error);
            _userRepository.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_NonExistingUser_ShouldFail()
        {
            // Arrange
            var request = new UpdateUserDto
            {
                Id = 0
            };

            // Act
            var response = await _userService.UpdateUser(request);

            // Asset
            Assert.False(response.IsSuccess);
            Assert.NotNull(response.Error);
        }

        [Fact]
        public async Task UpdateUser_ShouldSucceed()
        {
            // Arrange
            var existingUser = new User
            {
                Id = 1,
                Email = "test@email.com"
            };
            var request = new UpdateUserDto
            {
                Id = 1,
                Email = "newmail@test.com"
            };
            _userRepository.Setup(x => x.Get(request.Id))
                .Returns(Task.FromResult<User?>(existingUser));

            // Act
            var response = await _userService.UpdateUser(request);

            // Asset
            Assert.True(response.IsSuccess);
            Assert.Null(response.Error);
            _userRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }
    }
}
