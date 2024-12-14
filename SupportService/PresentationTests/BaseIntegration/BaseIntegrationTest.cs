using Application.Abstractions;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace PresentationTests.BaseIntegration;

[TestFixture]
public class BaseIntegrationTest
{
    private static IServiceScope _scope;
    protected static IntegrationTestWebAppFactory _factory;
    protected static ApplicationDbContext _dbContext;
    protected static IGroupService _groupService;
    protected static HttpClient _client;

    static BaseIntegrationTest()
    {
        _factory = new IntegrationTestWebAppFactory();
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _scope = _factory.Services.CreateScope();

        //Injections for tests goes here
        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _groupService = _scope.ServiceProvider.GetRequiredService<IGroupService>();
        _client = _factory.CreateClient();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _scope?.Dispose();
        _dbContext?.Dispose();
        _client?.Dispose();
    }

    protected async Task ExecuteInATransactionAsync(Func<Task> actionToExecute)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        
        await actionToExecute();

        await transaction.RollbackAsync();
    }
}
