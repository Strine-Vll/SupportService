using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.NotificationDtos;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class NotificationService
{
    public NotificationService(INotificationRepository notificationRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    private readonly INotificationRepository _notificationRepository;

    private readonly IMapper _mapper;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public async Task<List<NotificationDto>> GetUserNotifications(int userId)
    {
        var dbNotifications = await _notificationRepository.GetUserNotifications(userId);

        var notifications = _mapper.Map<List<NotificationDto>>(dbNotifications);

        return notifications;
    }

    public async Task CreateNotification(string title, string message, int? recipientId)
    {
        if (recipientId == null)
        {
            recipientId = await GetDefaultRecipient();
        }

        var notification = new Notification { Title = title, Message = message, UserId = recipientId.Value };

        await _notificationRepository.CreateAsync(notification);
    }

    public async Task<int> GetDefaultRecipient()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return 0;

        var userIdClaim = user.FindFirst("userId") ?? user.FindFirst(ClaimTypes.NameIdentifier);
        int userId;

        return int.TryParse(userIdClaim?.Value, out userId) ? userId : 0;
    }
}
