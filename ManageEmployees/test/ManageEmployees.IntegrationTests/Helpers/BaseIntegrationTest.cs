using ManageEmployees.Application;
using ManageEmployees.Infrastructure;
using ManageEmployees.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ManageEmployees.IntegrationTests.Helpers
{
    public abstract class BaseIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public BaseIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _client = factory.WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.AddApplicationServices();
                services.AddInfrastructureServices(_configuration);
                services.AddPersistenceServices(_configuration);
            }))
            .CreateClient();
        }
    }
}
