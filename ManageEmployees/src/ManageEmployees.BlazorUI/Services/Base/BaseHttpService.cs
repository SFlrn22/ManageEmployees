using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace ManageEmployees.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;
        protected readonly ILocalStorageService _localStorage;
        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }
        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException exception)
        {
            switch (exception.StatusCode)
            {
                case 400:
                    return new Response<Guid>()
                    {
                        Message = "Invalid data was submitted",
                        ValidationErrors = exception.Response,
                        Success = false
                    };
                case 404:
                    return new Response<Guid>()
                    {
                        Message = "The record was not found",
                        Success = false
                    };
                default:
                    return new Response<Guid>()
                    {
                        Message = "Something went wrong",
                        Success = false
                    };
            }
        }
        protected async Task AddBearerToken()
        {
            if (await _localStorage.ContainKeyAsync("token"))
                _client.HttpClient.DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Bearer",
                    await _localStorage.GetItemAsync<string>("token"));
        }
    }
}
