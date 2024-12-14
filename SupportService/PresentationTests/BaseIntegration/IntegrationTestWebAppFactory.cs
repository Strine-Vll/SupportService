using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupportService;
using Testcontainers.MsSql;

namespace PresentationTests.BaseIntegration;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();

    public IntegrationTestWebAppFactory()
    {
        _dbContainer.StartAsync().GetAwaiter().GetResult();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string connectionString = _dbContainer.GetConnectionString();
                options.UseSqlServer(connectionString);
            });
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContainer.StopAsync().GetAwaiter().GetResult();
        }

        base.Dispose(disposing);
    }
}
