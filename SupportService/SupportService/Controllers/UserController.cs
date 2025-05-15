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

        [HttpGet("UsersToManage")]
        public async Task<ActionResult> GetUsersToManage()
        {
            var result = await _userService.GetUsersToManage();

            return Ok(result);
        }

        [HttpGet("GetEditUser")]
        public async Task<ActionResult> GetEditUser(int userId)
        {
            var result = await _userService.GetEditUser(userId);

            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser(EditUserDto user)
        {
            await _userService.UpdateUser(user);

            return Ok();
        }

        [HttpPost("DeactivateUser")]
        public async Task<ActionResult> DeactivateUser(int userId)
        {
            await _userService.DeactivateUser(userId);

            return Ok();
        }
    }
}
