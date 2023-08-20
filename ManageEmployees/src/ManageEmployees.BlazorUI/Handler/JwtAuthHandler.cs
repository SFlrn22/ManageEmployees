using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace ManageEmployees.BlazorUI.Handler
{
    public class JwtAuthHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;
        public JwtAuthHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorageService.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
