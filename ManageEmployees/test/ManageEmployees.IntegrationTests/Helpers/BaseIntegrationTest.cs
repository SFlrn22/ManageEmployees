using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace ManageEmployees.IntegrationTests.Helpers
{
    public abstract class BaseIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;
        private readonly AuthHelper _authHelper;
        public BaseIntegrationTest(WebApplicationFactory<Program> factory)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _client = factory.CreateClient();
            _authHelper = new AuthHelper(configuration);
            var token = _authHelper.HandleAuth("User", "Employee", "user@localhost.com", "9107d66c-2b0c-4023-83d8-eb0a77a9d631");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
