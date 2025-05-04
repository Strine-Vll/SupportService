using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository;

    private IMapper _mapper;

    public AttachmentService(IAttachmentRepository attachmentRepository, IMapper mapper)
    {
        _attachmentRepository = attachmentRepository;
        _mapper = mapper;
    }

    public async Task<Attachment> GetAttachmentByIdAsync(int attachmentId)
    {
        var attachment = await _attachmentRepository.GetByIdAsync(attachmentId);

        return attachment;
    }
}
