using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace ManageEmployees.BlazorUI.Pages
{
    public partial class Register
    {
        public RegisterVM Request { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        public string Message { get; set; }

        [Inject]
        private IAuthenticationService _authenticationService { get; set; }

        protected override void OnInitialized()
        {
            Request = new RegisterVM();
        }

        protected async Task HandleRegister()
        {
            var result = await _authenticationService.RegisterAsync(Request.FirstName,
                Request.LastName, Request.UserName, Request.Email, Request.Password);

            if (result)
            {
                _navigationManager.NavigateTo("/");
            }
            Message = "Something went wrong, please try again.";
        }
    }
}