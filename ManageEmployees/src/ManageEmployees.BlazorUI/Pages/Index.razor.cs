using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ManageEmployees.BlazorUI.Pages
{
    public partial class Index
    {
        [Inject]
        private AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Inject]
        public IAuthenticationService _authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((APIAuthStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();
        }

        protected void GoToLogin()
        {
            _navigationManager.NavigateTo("login/");
        }

        protected void GoToRegister()
        {
            _navigationManager.NavigateTo("register/");
        }

        protected async void Logout()
        {
            await _authenticationService.Logout();
        }
    }
}