using Blazored.LocalStorage;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Providers;
using ManageEmployees.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace ManageEmployees.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _stateProvider;
        public AuthenticationService(IClient client, ILocalStorageService localStorage,
            AuthenticationStateProvider stateProvider) :
            base(client, localStorage)
        {
            _stateProvider = stateProvider;
        }
        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            AuthRequest authRequest = new AuthRequest()
            {
                Email = email,
                Password = password
            };
            var authResponse = await _client.LoginAsync(authRequest);
            if (authResponse.Token != string.Empty)
            {
                await _localStorage.SetItemAsync("token", authResponse.Token);

                await ((APIAuthStateProvider)_stateProvider).LoggedIn();

                return true;
            }
            return false;
        }
        public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email,
            string password)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password
            };
            var response = await _client.RegisterAsync(registrationRequest);
            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }
            return false;
        }
        public async Task Logout()
        {
            await ((APIAuthStateProvider)_stateProvider).LoggedOut();
        }
    }
}
