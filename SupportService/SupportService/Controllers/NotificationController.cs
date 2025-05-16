using Application.Abstractions;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SupportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<ActionResult> GetGroupRequestsPreview(int userId)
        {
            var result = await _notificationService.GetUserNotifications(userId);

            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<ActionResult> GetNotificationCount(int userId)
        {
            var result = await _notificationService.GetNotificationsCount(userId);

            return Ok(result);
        }
    }
}
