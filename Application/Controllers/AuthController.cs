using Application.BusinessLogic.Services.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterModel registerModel)
        {
            if (_authService.UserExists(registerModel.Login))
                return BadRequest("UserName Is Already Taken");
            if (!_authService.Register(registerModel)) return BadRequest();
            return Ok("user was registered");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginModel loginModel)
        {
            return Ok(_authService.Login(loginModel));
        }

        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put([FromBody] RefreshTokenModel model)
        {
            return Ok(_authService.Refresh(model));
        }
    }
}
