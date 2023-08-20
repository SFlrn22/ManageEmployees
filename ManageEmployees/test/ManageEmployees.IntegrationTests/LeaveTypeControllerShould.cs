using ManageEmployees.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace ManageEmployees.IntegrationTests
{
    public class LeaveTypeControllerShould : BaseIntegrationTest
    {
        public LeaveTypeControllerShould(WebApplicationFactory<Program> factory)
            : base(factory)
        {

        }
        [Fact]
        public async Task LeaveTypeGet_ReturnsOkWithContents()
        {
            //ARRANGE

            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVETYPES);

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
        }
        [Fact]
        public async Task LeaveTypeGetDetails_WhenValidId_ReturnsOkWithContents()
        {
            //ARRANGE
            var id = 1;
            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVETYPES.ToString() + ($"/{id}"));
            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
        }
        [Fact]
        public async Task LeaveTypeGetDetails_WhenInvalidId_ReturnsNotFound()
        {
            //ARRANGE
            var id = -1;
            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVETYPES.ToString() + ($"/{id}"));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();
            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            response.Content.ShouldNotBeNull();
            responseContent.Errors.ShouldBeEmpty();
            responseContent.Type.ShouldBe("NotFoundException");
            responseContent.Title.ShouldContain("was not found");
            responseContent.Status.ShouldBe(404);
        }
    }
}
