using Microsoft.AspNetCore.Mvc;
using TelaCompro.Application.Requests.User;
using TelaCompro.Application.Responses;
using TelaCompro.Application.Services.Interfaces;

namespace TelaCompro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<Result> Register(RegisterDto request)
        {
            var response = await _userService.Register(request);
            return response;
        }
    }
}
