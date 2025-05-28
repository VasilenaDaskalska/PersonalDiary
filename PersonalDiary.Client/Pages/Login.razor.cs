using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PersonalDiary.Business.Models.Users;
using PersonalDiary.Client.Shared.State;
using PersonalDiary.HttpRepositories.Contracts;
using PersonalDiary.Mappers;

namespace PersonalDiary.Client.Pages;

public partial class Login
{
    [Inject]
    private IUserHttpRepository UserRepository { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ILocalStorageService LocalStorage { get; set; }

    [Inject]
    private UserState UserState { get; set; }

    [Inject]
    private ISnackbar Snackbar { get; set; }

    private MudForm form;
    private bool success;
    private SignInRequestModel loginModel = new();

    private async Task HandleLogin()
    {
        try
        {
            var result = await this.UserRepository.SignInAsync(this.loginModel.Username, this.loginModel.Password);
            if (result != null)
            {
                await this.LocalStorage.SetItemAsync("authToken", result.Token);
                var userForSession = UserMapper.MapUserEntityFromUserBiz(result.User);
                await this.UserState.SetUserAsync(userForSession);
                this.NavigationManager.NavigateTo("/");
            }
        }
        catch (Exception ex)
        {
            this.Snackbar.Add("Login failed. Please check your credentials.", Severity.Error);
        }
    }
}