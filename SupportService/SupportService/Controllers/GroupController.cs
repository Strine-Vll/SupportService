using Application.Abstractions;
using Application.Dtos.GroupDtos;
using Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupportService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet("groups")]
    public async Task<ActionResult> GetUserGroups(int userId)
    {
        var groups = await _groupService.GetUserGroups(userId);

        return Ok(groups);
    }

    [HttpGet]
    public async Task<ActionResult> GetGroup(int groupId)
    {
        var group = await _groupService.GetGroupById(groupId);

        return Ok(group);
    }

    [HttpPost("updateUsers")]
    public async Task<ActionResult> UpdateUsers([FromQuery] int groupId, [FromBody] List<UserPreviewDto> users)
    {
        await _groupService.UpdateUsers(groupId, users);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> CreateGroup(GroupDto groupDto, int userId)
    {
        await _groupService.CreateGroup(groupDto, userId);

        return Ok();
    }

    [HttpPost("Invite")]
    public async Task<ActionResult> InviteUser(int groupId, int userId)
    {
        await _groupService.InviteUser(groupId, userId);

        return Ok();
    }

    [HttpPost("RemoveUser")]
    public async Task<ActionResult> RemoveUserFromGroup(int groupId, int userId)
    {
        await _groupService.RemoveUserFromGroup(groupId, userId);

        return Ok();
    }

    [HttpPut("update")]
    public async Task<ActionResult> RemoveUserFromGroup(GroupDto group)
    {
        await _groupService.UpdateGroup(group);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteGroup(int groupId)
    {
        await _groupService.DeleteGroup(groupId);

        return Ok();
    }
}
