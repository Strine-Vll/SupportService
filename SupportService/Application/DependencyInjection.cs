using Application.Abstractions;
using Application.Authentication;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddValidatorsFromAssembly(assembly);
        services.AddAutoMapper(assembly);
        services.AddHttpContextAccessor();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IServiceRequestService, ServiceRequestService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IAttachmentService, AttachmentService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IServiceRequestStatsService, ServiceRequestStatsService>();

        return services;
    }
}
