using Blazored.LocalStorage;
using MudBlazor.Services;
using PersonalDiary.Client.Shared.State;
using PersonalDiary.Components;
using PersonalDiary.HttpRepositories;
using PersonalDiary.HttpRepositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<UserState>();

var apiBaseAddress = new Uri("http://localhost:5271/");

builder.Services.AddHttpClient<IUserHttpRepository, UserHttpRepository>(client =>
{
    client.BaseAddress = apiBaseAddress;
});

// Configure HTTP clients
builder.Services.AddHttpClient<IAuthRepository, AuthRepository>();

builder.Services.AddHttpClient<IDiaryHttpRepository, DiaryHttpRepository>(client =>
{
    client.BaseAddress = apiBaseAddress;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PersonalDiary.Client._Imports).Assembly);

app.Run();
