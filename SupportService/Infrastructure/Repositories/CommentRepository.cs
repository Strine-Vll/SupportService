using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Comment>> GetCommentsByServiceRequest(int serviceRequestId)
    {
        var result = await _dbContext.Comments
            .Where(c => c.ServiceRequestId == serviceRequestId)
            .Include(c => c.CreatedBy)
            .Include(c => c.Attachments)
            .ToListAsync();

        return result;
    }
}
