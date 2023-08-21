using ManageEmployees.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;

namespace ManageEmployees.IntegrationTests
{
    public class LeaveRequestsControllerShould : BaseIntegrationTest
    {
        public LeaveRequestsControllerShould(WebApplicationFactory<Program> factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task GetLeaveRequest_ShouldReturnOk()
        {
            //ARRANGE

            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVEREQUESTS);

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetLeaveRequest_WhenValidId_ReturnsOkWithContents()
        {
            //ARRANGE
            var id = 1;

            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVEREQUESTS.ToString() + ($"/{id}"));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
        }
    }
}
