using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstractions;

public interface INotificationRepository : IBaseRepository<Notification>
{
    Task<List<Notification>> GetUserNotifications(int userId);

    Task<int> GetNotificationCount(int userId);

    Task CreateCommentNotification(int requestId, int createdById);
}
