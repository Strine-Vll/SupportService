using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<List<Comment>> GetCommentsByServiceRequest(int serviceRequestId);
}
