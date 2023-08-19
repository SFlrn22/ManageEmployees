using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace ManageEmployees.BlazorUI.Pages
{
    public partial class Login
    {
        public LoginVM Request { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }
        public string Message { get; set; }

        [Inject]
        private IAuthenticationService _authenticationService { get; set; }

        public Login()
        {

        }

        protected override void OnInitialized()
        {
            Request = new LoginVM();
        }

        protected async Task HandleLogin()
        {
            if (await _authenticationService.AuthenticateAsync(Request.Email, Request.Password))
            {
                _navigationManager.NavigateTo("/");
            }
            Message = "Username/password combination unknown";
        }
    }
}