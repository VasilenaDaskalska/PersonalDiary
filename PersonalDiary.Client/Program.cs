using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PersonalDiary.Client.Shared.State;
using PersonalDiary.HttpRepositories;
using PersonalDiary.HttpRepositories.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

var apiBaseAddress = new Uri("http://localhost:5271/");

// Configure HTTP clients
builder.Services.AddHttpClient<IAuthRepository, AuthRepository>();

builder.Services.AddHttpClient<IUserHttpRepository, UserHttpRepository>(client =>
{
    client.BaseAddress = apiBaseAddress;
});

builder.Services.AddHttpClient<IDiaryHttpRepository, DiaryHttpRepository>(client =>
{
    client.BaseAddress = apiBaseAddress;
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<UserState>();

await builder.Build().RunAsync();
