﻿using Application.Dtos.ServiceRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IServiceRequestService
{
    Task<List<ServiceRequestPreviewDto>> GetGroupRequestsPreview(int groupId);

    Task<ServiceRequestDto> GetServiceRequestOverview(int requestId);

    Task<EditServiceRequestDto> GetEditRequest(int requestId);

    Task CreateRequest(CreateRequestDto serviceRequest);
    
    Task UpdateRequest(EditServiceRequestDto serviceRequest);
}
