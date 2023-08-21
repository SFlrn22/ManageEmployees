using ManageEmployees.Application.Models.Identity;
using ManageEmployees.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace ManageEmployees.IntegrationTests
{
    public class AuthControllerShould : BaseIntegrationTest
    {
        public AuthControllerShould(WebApplicationFactory<Program> factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task Login_WhenValidPayload_ReturnsOk()
        {
            //ARRANGE
            AuthRequest payload = new AuthRequest
            {
                Email = "user@localhost.com",
                Password = "UserPassw@rd"
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.AUTH_LOGIN, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
        }

        [Fact]
        public async Task Login_WhenPayloadEmpty_ReturnsBadRequest()
        {
            //ARRANGE
            AuthRequest payload = new AuthRequest
            {
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.AUTH_LOGIN, HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            responseContent.Errors.Count.ShouldBe(2);
            responseContent.Title.ShouldBe("One or more validation errors occurred.");
            var errors = responseContent.Errors.ShouldBeAssignableTo<Dictionary<string, string[]>>();
            errors.ShouldContainKey("Password");
            errors.ShouldContainKey("Email");
        }

        [Fact]
        public async Task Login_WhenWrongUser_ReturnsNotFound()
        {
            //ARRANGE
            AuthRequest payload = new AuthRequest
            {
                Email = "test@localhost.com",
                Password = "test"
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.AUTH_LOGIN, HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            responseContent.Errors.Count.ShouldBe(0);
            responseContent.Title.ShouldContain("not found");
        }

        [Fact]
        public async Task Login_WhenWrongCredentials_ReturnsBadRequest()
        {
            //ARRANGE
            AuthRequest payload = new AuthRequest
            {
                Email = "user@localhost.com",
                Password = "test"
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.AUTH_LOGIN, HttpHelper.GetJsonHttpContent(payload));
            var responseContent = await response.Content.ReadFromJsonAsync<ApiExceptionModel>();

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            responseContent.Title.ShouldBe("Credentials aren't valid");
            responseContent.Type.ShouldBe("BadRequestException");
        }

        [Fact]
        public async Task Register_WhenValidPayload_ReturnsOk()
        {
            //ARRANGE
            RegistrationRequest payload = new RegistrationRequest
            {
                FirstName = "testabcd",
                LastName = "testabcd",
                Email = "testabcd@localhost.com",
                UserName = "testabcd",
                Password = "UserPassw@rd12345"
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.AUTH_REGISTER, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.ShouldNotBeNull();
        }

        [Fact]
        public async Task Register_WhenPassordContainsNoDigit_ReturnsBadRequest()
        {
            //ARRANGE
            RegistrationRequest payload = new RegistrationRequest
            {
                FirstName = "testabcd",
                LastName = "testabcd",
                Email = "testabcd@localhost.com",
                UserName = "testabcd",
                Password = "UserPassw@rd"
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.AUTH_REGISTER, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Register_WhenUserAlreadyExists_ReturnsBadRequest()
        {
            //ARRANGE
            RegistrationRequest payload = new RegistrationRequest
            {
                FirstName = "testabcd",
                LastName = "testabcd",
                Email = "user@localhost.com",
                UserName = "testabcd",
                Password = "UserPassw@rd1234"
            };

            //ACT
            var response = await _client.PostAsync(HttpHelper.Urls.AUTH_REGISTER, HttpHelper.GetJsonHttpContent(payload));

            //ASSERT
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
