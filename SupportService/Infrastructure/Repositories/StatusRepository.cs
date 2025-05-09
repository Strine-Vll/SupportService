using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class StatusRepository : BaseRepository<Status>, IStatusRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StatusRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
