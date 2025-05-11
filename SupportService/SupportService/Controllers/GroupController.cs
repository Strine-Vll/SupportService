using Application.Abstractions;
using Application.Dtos.GroupDtos;
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

    [HttpGet]
    public async Task<ActionResult> GetUserGroups(int userId)
    {
        var groups = await _groupService.GetUserGroups(userId);

        return Ok(groups);
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

    [HttpDelete]
    public async Task<ActionResult> DeleteGroup(int groupId)
    {
        await _groupService.DeleteGroup(groupId);

        return Ok();
    }
}
