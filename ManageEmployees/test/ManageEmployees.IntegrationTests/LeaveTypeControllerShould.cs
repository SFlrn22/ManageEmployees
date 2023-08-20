using ManageEmployees.Application.Features.LeaveType.Commands.CreateLeaveType;
using ManageEmployees.Application.Features.LeaveType.Commands.UpdateLeaveType;
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
        public async Task GetLeaveType_ReturnsOkWithContents()
        {
            //ARRANGE

            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVETYPES);

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetLeaveTypeDetails_WhenValidId_ReturnsOkWithContents()
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
        public async Task GetLeaveTypeDetails_WhenInvalidId_ReturnsNotFound()
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

        [Fact]
        public async Task PostLeaveType_WhenPayloadValid_ReturnsNoContent()
        {
            //ARRANGE
            CreateLeaveTypeCommand payload = new CreateLeaveTypeCommand
            {
                Name = "Test0",
                DefaultDays = 5
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.LEAVETYPES, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Fact]
        public async Task PostLeaveType_WhenMissingName_ReturnsBadRequest()
        {
            //ARRANGE
            CreateLeaveTypeCommand payload = new CreateLeaveTypeCommand
            {
                DefaultDays = 5
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.LEAVETYPES, HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            responseContent.Errors.Count.ShouldBe(1);
            responseContent.Errors.ShouldContainKey("Name");
            responseContent.Type.ShouldBe("BadRequestException");
            responseContent.Title.ShouldBe("Invalid LeaveType");
        }

        [Fact]
        public async Task PostLeaveType_WhenEmptyPayload_ReturnsBadRequest()
        {
            //ARRANGE
            CreateLeaveTypeCommand payload = new CreateLeaveTypeCommand
            {
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.LEAVETYPES, HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();
            //ASSERT
            var errors = responseContent.Errors.ShouldBeAssignableTo<Dictionary<string, string[]>>();
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            errors.Count.ShouldBe(2);
            errors.ShouldContainKey("Name");
            errors["DefaultDays"].Count().ShouldBe(2);
            responseContent.Type.ShouldBe("BadRequestException");
            responseContent.Title.ShouldBe("Invalid LeaveType");
        }

        [Fact]
        public async Task PostLeaveType_WhenDefaultDaysNegative_ReturnsBadRequest()
        {
            //ARRANGE
            CreateLeaveTypeCommand payload = new CreateLeaveTypeCommand
            {
                Name = "Testabcde",
                DefaultDays = -1
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.LEAVETYPES, HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            responseContent.Errors.Count.ShouldBe(1);
            responseContent.Errors.ShouldContainKey("DefaultDays");
            responseContent.Type.ShouldBe("BadRequestException");
            responseContent.Title.ShouldBe("Invalid LeaveType");
        }

        [Fact]
        public async Task PostLeaveType_WhenAlreadyExsists_ReturnsBadRequest()
        {
            //ARRANGE
            CreateLeaveTypeCommand payload = new CreateLeaveTypeCommand
            {
                Name = "Test",
                DefaultDays = 5
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.LEAVETYPES, HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            responseContent.Errors.Count.ShouldBe(1);
            responseContent.Type.ShouldBe("BadRequestException");
            responseContent.Title.ShouldBe("Invalid LeaveType");
        }

        [Fact]
        public async Task UpdateLeaveType_WhenValidPayload_ReturnsNoContent()
        {
            //ARRANGE
            UpdateLeaveTypeCommand payload = new UpdateLeaveTypeCommand
            {
                Name = "TestUpdated",
                DefaultDays = 5,
                Id = 1
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVETYPES.ToString() + $"/{payload.Id}", HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task UpdateLeaveType_WhenEmptyPayload_ReturnsBadRequest()
        {
            //ARRANGE
            UpdateLeaveTypeCommand payload = new UpdateLeaveTypeCommand
            {

            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVETYPES.ToString() + $"/{payload.Id}", HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            responseContent.Errors.Count.ShouldBe(2);
            responseContent.Type.ShouldBe("BadRequestException");
            responseContent.Title.ShouldBe("Invalid LeaveType");
        }

        [Fact]
        public async Task UpdateLeaveType_WhenInvalidId_ReturnsBadRequest()
        {
            //ARRANGE
            UpdateLeaveTypeCommand payload = new UpdateLeaveTypeCommand
            {
                Name = "UpdateTest",
                DefaultDays = 5,
                Id = -1
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVETYPES.ToString() + $"/{payload.Id}", HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            responseContent.Errors.Count.ShouldBe(1);
            responseContent.Type.ShouldBe("BadRequestException");
            responseContent.Title.ShouldBe("Invalid LeaveType");
        }

        [Fact]
        public async Task UpdateLeaveType_WhenLeaveTypeAlreadyExists_ReturnsBadRequest()
        {
            //ARRANGE
            UpdateLeaveTypeCommand payload = new UpdateLeaveTypeCommand
            {
                Name = "TestUpdated",
                DefaultDays = 5,
                Id = 1
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVETYPES.ToString() + $"/{payload.Id}", HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            responseContent.Errors.Count.ShouldBe(1);
            responseContent.Type.ShouldBe("BadRequestException");
            responseContent.Title.ShouldBe("Invalid LeaveType");
        }

        [Fact]
        public async Task DeleteLeaveType_WhenValidId_ReturnsNoContent()
        {
            //ARRANGE
            var id = 3;

            //ACT
            var response = await _client.DeleteAsync(HttpHelper.Urls.LEAVETYPES.ToString() + ($"/{id}"));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteLeaveType_WhenInvalidId_ReturnsNotFound()
        {
            //ARRANGE
            var id = -1;

            //ACT
            var response = await _client.DeleteAsync(HttpHelper.Urls.LEAVETYPES.ToString() + ($"/{id}"));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
