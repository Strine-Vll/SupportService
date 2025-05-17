using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.NotificationDtos;

namespace Application.Abstractions;

public interface INotificationService
{
    Task<List<NotificationDto>> GetUserNotifications(int userId);

    Task<int> GetNotificationsCount(int userId);

    Task CreateNotification(string title, string message, int? recipientId);

    Task<int> GetDefaultRecipient();

    Task DeleteNotification(int recipientId);
}
