using ManageEmployees.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using ManageEmployees.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using ManageEmployees.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;

namespace ManageEmployees.IntegrationTests
{
    public class LeaveAllocationsControllerShould : BaseIntegrationTest
    {
        public LeaveAllocationsControllerShould(WebApplicationFactory<Program> factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task GetLeaveAllocations_ShouldReturnOk()
        {
            //ARRANGE

            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVEALLOCATIONS);

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetLeaveAllocation_WhenValidId_ReturnsOkWithContents()
        {
            //ARRANGE
            var id = 1;

            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVEALLOCATIONS.ToString() + ($"/{id}"));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetLeaveAllocation_WhenInvalidId_ReturnsNoContent()
        {
            //ARRANGE
            var id = 100;

            //ACT
            var response = await _client.GetAsync(HttpHelper.Urls.LEAVEALLOCATIONS.ToString() + ($"/{id}"));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task PostLeaveAllocation_WhenValidPayload_ReturnsCreated()
        {
            //ARRANGE
            CreateLeaveAllocationCommand payload = new CreateLeaveAllocationCommand
            {
                LeaveTypeId = 1
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.LEAVEALLOCATIONS, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Fact]
        public async Task PutLeaveAllocation_WhenValidPayload_ReturnsNoContent()
        {
            //ARRANGE
            UpdateLeaveAllocationCommand payload = new UpdateLeaveAllocationCommand
            {
                Id = 2,
                LeaveTypeId = 1,
                NumberOfDays = 20,
                Period = 2023
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVEALLOCATIONS, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task PutLeaveAllocation_WhenInvalidPayload_ReturnsBadRequest()
        {
            //ARRANGE
            UpdateLeaveAllocationCommand payload = new UpdateLeaveAllocationCommand
            {
                Id = 2,
                LeaveTypeId = 1,
                NumberOfDays = 0,
                Period = 2
            };

            //ACT
            var response = await _client.PutAsync(HttpHelper.Urls.LEAVEALLOCATIONS, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteLeaveAllocation_WhenValidId_ReturnsNoContent()
        {
            //ARRANGE
            var id = 3;

            //ACT
            var response = await _client.DeleteAsync(HttpHelper.Urls.LEAVEALLOCATIONS.ToString() + ($"/{id}"));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteLeaveAllocation_WhenInvalidId_ReturnsNotFound()
        {
            //ARRANGE
            var id = -1;

            //ACT
            var response = await _client.DeleteAsync(HttpHelper.Urls.LEAVEALLOCATIONS.ToString() + ($"/{id}"));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
