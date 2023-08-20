using Blazored.LocalStorage;
using Blazored.Toast;
using ManageEmployees.BlazorUI;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Handler;
using ManageEmployees.BlazorUI.Providers;
using ManageEmployees.BlazorUI.Services;
using ManageEmployees.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<JwtAuthHandler>();
builder.Services.AddHttpClient<IClient, Client>(Client => Client.BaseAddress = new Uri("https://localhost:7290"))
    .AddHttpMessageHandler<JwtAuthHandler>();

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<APIAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, APIAuthStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();
