
using ManageEmployees.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using ManageEmployees.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using ManageEmployees.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using ManageEmployees.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using ManageEmployees.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

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

        [Fact]
        public async Task GetLeaveRequest_WhenInvalidId_ReturnsNotFoundException()
        {
            //ARRANGE
            var id = 100;

            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVEREQUESTS.ToString() + ($"/{id}"));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            response.Content.ShouldNotBeNull();
            responseContent.Errors.ShouldBeEmpty();
            responseContent.Type.ShouldBe("NotFoundException");
            responseContent.Title.ShouldContain("was not found");
            responseContent.Status.ShouldBe(404);
        }

        [Fact]
        public async Task PostLeaveRequest_WhenNotAllocated_ReturnsBadRequest()
        {
            //ARRANGE
            CreateLeaveRequestCommand payload = new CreateLeaveRequestCommand
            {
                StartDate = DateTime.Parse("2023-08-22T07:08:09.071Z"),
                EndDate = DateTime.Parse("2023-08-24T07:08:09.071Z"),
                LeaveTypeId = 1,
                RequestComments = "abcdefgh"
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.LEAVEREQUESTS, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PostLeaveRequest_WhenNotEnoughDays_ReturnsBadRequest()
        {
            //ARRANGE
            CreateLeaveRequestCommand payload = new CreateLeaveRequestCommand
            {
                StartDate = DateTime.Parse("2023-08-22T07:08:09.071Z"),
                EndDate = DateTime.Parse("2023-12-24T07:08:09.071Z"),
                LeaveTypeId = 1,
                RequestComments = "abcdefgh"
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.LEAVEREQUESTS, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PutLeaveRequest_WhenValidPayload_ReturnsNoContent()
        {
            //ARRANGE
            UpdateLeaveRequestCommand payload = new UpdateLeaveRequestCommand
            {
                Id = 1,
                Cancelled = false,
                StartDate = DateTime.Parse("2023-08-22T07:08:09.071Z"),
                EndDate = DateTime.Parse("2023-08-24T07:08:09.071Z"),
                LeaveTypeId = 1,
                RequestComments = "asdasdasda"

            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVEREQUESTS, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task PutLeaveRequest_WhenInvalidPayload_ReturnsBadRequest()
        {
            //ARRANGE
            UpdateLeaveRequestCommand payload = new UpdateLeaveRequestCommand
            {
                Id = 1,
                Cancelled = false,
                StartDate = DateTime.Parse("2023-08-24T07:08:09.071Z"),
                EndDate = DateTime.Parse("2023-08-22T07:08:09.071Z"),
                LeaveTypeId = 1,
                RequestComments = "asdasdasda"

            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVEREQUESTS, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteLeaveRequest_WhenValidId_ReturnsNoContent()
        {
            //ARRANGE
            var id = 4;

            //ACT
            var response = await _client.DeleteAsync(HttpHelper.Urls.LEAVEREQUESTS.ToString() + ($"/{id}"));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteLeaveRequest_WhenInvalidId_ReturnsNotFound()
        {
            //ARRANGE
            var id = -1;

            //ACT
            var response = await _client.DeleteAsync(HttpHelper.Urls.LEAVEREQUESTS.ToString() + ($"/{id}"));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CancelLeaveRequest_WhenValidPayload_ReturnNoContent()
        {
            //ARRANGE
            CancelLeaveRequestCommand payload = new CancelLeaveRequestCommand
            {
                Id = 2
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVEREQUESTS.ToString() + "/CancelRequest", HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task CancelLeaveRequest_WhenInvalidId_ReturnNotFound()
        {
            //ARRANGE
            CancelLeaveRequestCommand payload = new CancelLeaveRequestCommand
            {
                Id = -1
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVEREQUESTS.ToString() + "/CancelRequest", HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateApprovalLeaveRequest_WhenValidPayload_ReturnNoContent()
        {
            //ARRANGE
            ChangeLeaveRequestApprovalCommand payload = new ChangeLeaveRequestApprovalCommand
            {
                Id = 2,
                Approved = true
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVEREQUESTS.ToString() + "/UpdateApproval", HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task UpdateApprovalLeaveRequest_WhenInvalidId_ReturnsNotFound()
        {
            //ARRANGE
            ChangeLeaveRequestApprovalCommand payload = new ChangeLeaveRequestApprovalCommand
            {
                Id = -1,
                Approved = true
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVEREQUESTS.ToString() + "/UpdateApproval", HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateApprovalLeaveRequest_WhenInvalidPayload_ReturnsBadRequest()
        {
            //ARRANGE
            ChangeLeaveRequestApprovalCommand payload = new ChangeLeaveRequestApprovalCommand
            {
                Id = 1
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVEREQUESTS.ToString() + "/UpdateApproval", HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
