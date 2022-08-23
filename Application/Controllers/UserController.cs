using Application.BusinessLogic.Services.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var user = _userService.Get(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.Get();
            return Ok(users);
        }
        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok("Пользователь был удалён");
        }
        [HttpPut]
        [Authorize]
        public IActionResult Edit(int userId, UpdateUserModel model)
        {
            _userService.Edit(userId, model);
            return Ok("Пользователь был изменён");
        }

    }
}
