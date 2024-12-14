using Application.Dtos.ServiceRequestDtos;
using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests.Services;

[TestFixture]
public class ServiceRequestServiceTests
{
    private ServiceRequestService _serviceRequestService;
    private Mock<IMapper> _mapperMock;
    private Mock<IServiceRequestRepository> _serviceRequestRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceRequestRepositoryMock = new Mock<IServiceRequestRepository>();
        _serviceRequestService = new ServiceRequestService(_mapperMock.Object, _serviceRequestRepositoryMock.Object);
    }

    [Test]
    public async Task GetGroupRequestsPreview_ShouldReturnMappedRequests_WhenRequestsExist()
    {
        // Arrange
        var groupId = 1;
        var serviceRequests = new List<ServiceRequest>
        {
            new ServiceRequest { Id = 1, Title = "Request 1" },
            new ServiceRequest { Id = 2, Title = "Request 2" }
        };

        var expectedDtos = new List<ServiceRequestPreviewDto>
        {
            new ServiceRequestPreviewDto { Id = 1, Title = "Request 1" },
            new ServiceRequestPreviewDto { Id = 2, Title = "Request 2" }
        };

        _serviceRequestRepositoryMock.Setup(repo => repo.GetByGroup(groupId)).ReturnsAsync(serviceRequests);
        _mapperMock.Setup(m => m.Map<List<ServiceRequestPreviewDto>>(serviceRequests)).Returns(expectedDtos);

        // Act
        var result = await _serviceRequestService.GetGroupRequestsPreview(groupId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(expectedDtos[0].Id, result[0].Id);
        Assert.AreEqual(expectedDtos[1].Title, result[1].Title);
    }

    [Test]
    public async Task GetServiceRequestOverview_ShouldReturnMappedRequest_WhenRequestExists()
    {
        // Arrange
        var requestId = 1;
        var serviceRequest = new ServiceRequest
        {
            Id = requestId,
            Title = "Request Title",
            Description = "Request Description",
            CreatedDate = DateTime.UtcNow,
            CreatedById = 1
        };

        var expectedDto = new ServiceRequestDto
        {
            Id = requestId,
            Title = "Request Title",
            Description = "Request Description",
            CreatedDate = serviceRequest.CreatedDate,
            CreatedBy = "User"
        };

        _serviceRequestRepositoryMock.Setup(repo => repo.GetByIdAsync(requestId)).ReturnsAsync(serviceRequest);
        _mapperMock.Setup(m => m.Map<ServiceRequestDto>(serviceRequest)).Returns(expectedDto);

        // Act
        var result = await _serviceRequestService.GetServiceRequestOverview(requestId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedDto.Id, result.Id);
        Assert.AreEqual(expectedDto.Title, result.Title);
    }


    [Test]
    public void GetServiceRequestOverview_ShouldThrowServiceRequestNotFoundException_WhenRequestDoesNotExist()
    {
        // Arrange
        var requestId = 1;

        _serviceRequestRepositoryMock.Setup(repo => repo.GetByIdAsync(requestId)).ReturnsAsync((ServiceRequest)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<ServiceRequestNotFoundException>(async () =>
            await _serviceRequestService.GetServiceRequestOverview(requestId));

        // Assert the type of exception
        Assert.That(ex, Is.InstanceOf<ServiceRequestNotFoundException>());
    }
}
