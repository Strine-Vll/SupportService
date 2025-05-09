using Domain.Entities;

namespace Application.Abstractions;

public interface IStatusService
{
    Task<List<Status>> GetStatusesAsync();
}
