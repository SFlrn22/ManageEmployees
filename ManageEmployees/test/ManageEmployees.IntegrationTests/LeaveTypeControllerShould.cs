using ManageEmployees.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;

namespace ManageEmployees.IntegrationTests
{
    public class LeaveTypeControllerShould : BaseIntegrationTest
    {
        public LeaveTypeControllerShould(WebApplicationFactory<Program> factory)
            : base(factory)
        {

        }
        [Fact]
        public async Task EmployeeGet_ReturnsOkWithContents()
        {
            //ARRANGE

            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.EMPLOYEES);

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
        }
    }
}
