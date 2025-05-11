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
            CreatedBy = new User { Name = "User" }
        };

        var expectedDto = new ServiceRequestDto
        {
            Id = requestId,
            Title = "Request Title",
            Description = "Request Description",
            CreatedDate = serviceRequest.CreatedDate,
            CreatedBy = serviceRequest.CreatedBy.Name
        };

        _serviceRequestRepositoryMock.Setup(repo => repo.GetOverviewById(requestId)).ReturnsAsync(serviceRequest);
        _mapperMock.Setup(m => m.Map<ServiceRequestDto>(serviceRequest)).Returns(expectedDto);

        // Act
        var result = await _serviceRequestService.GetServiceRequestOverview(requestId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedDto.Id, result.Id);
        Assert.AreEqual(expectedDto.Title, result.Title);
        Assert.AreEqual(expectedDto.Description, result.Description);
        Assert.AreEqual(expectedDto.CreatedBy, result.CreatedBy);
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

    [Test]
    public async Task CreateRequest_ShouldCreateServiceRequest()
    {
        // Arrange
        var createRequestDto = new CreateRequestDto
        {
            Title = "Test Title",
            Description = "Test Description",
            GroupId = 1,
            CreatedById = 1
        };

        var dbRequest = new ServiceRequest
        {
            Title = createRequestDto.Title,
            Description = createRequestDto.Description,
            GroupId = createRequestDto.GroupId,
            CreatedById = createRequestDto.CreatedById
        };

        _mapperMock.Setup(m => m.Map<ServiceRequest>(createRequestDto)).Returns(dbRequest);

        // Act
        await _serviceRequestService.CreateRequest(createRequestDto);

        // Assert
        _serviceRequestRepositoryMock.Verify(repo => repo.CreateAsync(dbRequest), Times.Once);
    }

    [Test]
    public async Task UpdateRequest_ShouldUpdateServiceRequest()
    {
        // Arrange
        var updateRequestDto = new EditServiceRequestDto
        {
            Id = 1,
            Title = "Updated Title",
            Description = "Updated Description",
            Status = new Status(),
            Appointed = null
        };

        var existingRequest = new ServiceRequest
        {
            Id = 1,
            Title = "Old Title",
            Description = "Old Description",
            GroupId = 1,
            AppointedId = 1,
            Status = new Status()
        };

        _serviceRequestRepositoryMock.Setup(repo => repo.GetRequestForUpdate(updateRequestDto.Id))
            .ReturnsAsync(existingRequest);

        _mapperMock.Setup(m => m.Map<ServiceRequest>(updateRequestDto)).Returns(existingRequest);

        // Act
        await _serviceRequestService.UpdateRequest(updateRequestDto);

        // Assert
        Assert.AreEqual("Updated Title", existingRequest.Title);
        Assert.AreEqual("Updated Description", existingRequest.Description);
        Assert.AreEqual(2, existingRequest.GroupId);
        Assert.AreEqual(3, existingRequest.AppointedId);
        _serviceRequestRepositoryMock.Verify(repo => repo.UpdateAsync(existingRequest), Times.Once);
    }
}
