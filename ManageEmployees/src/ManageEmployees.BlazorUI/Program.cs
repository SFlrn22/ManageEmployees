using ManageEmployees.BlazorUI;
using ManageEmployees.BlazorUI.Contracts;
using ManageEmployees.BlazorUI.Services;
using ManageEmployees.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IClient, Client>(Client => Client.BaseAddress = new Uri("http://localhost:4811"));
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

await builder.Build().RunAsync();
