using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public NotificationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Notification>> GetUserNotifications(int userId)
    {
        var result = await _dbContext.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();

        return result;
    }

    public async Task<int> GetNotificationCount(int userId)
    {
        var result = await _dbContext.Notifications
            .Where(n => n.UserId == userId)
            .CountAsync();

        return result;
    }

    public async Task CreateCommentNotification(int requestId, int createdById)
    {
        var request = await _dbContext.ServiceRequests
            .Include(sr => sr.Appointed)
            .Include(sr => sr.CreatedBy)
            .FirstOrDefaultAsync(sr => sr.Id == requestId);

        if (request != null)
        {
            const string title = "Новый комментарий";

            string message = $"В заявке '{request.Title}' был оставлен новый комментарий";

            if (createdById != request.Appointed.Id)
            {
                await _dbContext.Notifications.AddAsync(new Notification 
                { 
                    Title = title, 
                    Message = message,
                    UserId = request.Appointed.Id
                });
            }
            if (createdById != request.CreatedBy.Id)
            {
                await _dbContext.Notifications.AddAsync(new Notification
                {
                    Title = title,
                    Message = message,
                    UserId = request.CreatedBy.Id
                });
            }
        }
    }
}
