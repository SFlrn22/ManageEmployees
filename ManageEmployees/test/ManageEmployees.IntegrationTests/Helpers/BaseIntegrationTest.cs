using Microsoft.AspNetCore.Mvc.Testing;

namespace ManageEmployees.IntegrationTests.Helpers
{
    public abstract class BaseIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;
        public BaseIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
    }
}
