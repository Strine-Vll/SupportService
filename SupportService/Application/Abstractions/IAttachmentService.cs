using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions;

public interface IAttachmentService
{
    Task<Attachment> GetAttachmentByIdAsync(int requestId);
}
