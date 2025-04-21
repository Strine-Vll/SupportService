using Application.Abstractions;
using Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupportService.Controllers
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

        [HttpGet("GroupUsers")]
        public async Task<ActionResult> GroupUsers(int groupId)
        {
            var result = await _userService.GetGroupUsersAsync(groupId);

            return Ok(result);
        }
    }
}
