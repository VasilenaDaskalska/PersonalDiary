using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PersonalDiary.Business.Models.Users;
using PersonalDiary.Client.Shared.State;
using PersonalDiary.HttpRepositories.Contracts;

namespace PersonalDiary.Client.Pages;

public partial class Register
{

    private MudForm form;
    private bool success;
    private UserBiz registerModel = new();
    private string confirmPassword;

    [Inject]
    private IUserHttpRepository UserRepository { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private UserState UserState { get; set; }

    [Inject]
    private ISnackbar Snackbar { get; set; }

    protected Func<string, Task<IEnumerable<string>>> ValidatePassword => async (password) =>
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(password))
        {
            errors.Add("Password is required.");
            return await Task.FromResult(errors.AsEnumerable());
        }

        // Regex explanation:
        // ^                         : start of string
        // (?=.*[a-z])               : at least one lowercase letter
        // (?=.*[A-Z])               : at least one uppercase letter
        // (?=.*[^a-zA-Z0-9])        : at least one symbol (non-alphanumeric)
        // .{8,}                     : at least 8 characters long
        // $                         : end of string
        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$");

        if (!regex.IsMatch(password))
        {
            errors.Add("Password must be at least 8 characters and include at least one uppercase letter, one lowercase letter, and one symbol.");
        }

        return await Task.FromResult(errors.AsEnumerable());
    };

    private async Task HandleRegister()
    {
        if (this.registerModel.Password != this.confirmPassword)
        {
            this.Snackbar.Add("Passwords do not match.", Severity.Error);
            return;
        }

        try
        {
            var result = await this.UserRepository.RegisterUserAsync(this.registerModel);

            if (result != null)
            {
                this.Snackbar.Add("Registration successful! Please login.", Severity.Success);
                this.NavigationManager.NavigateTo("/login");
            }
        }
        catch (Exception ex)
        {
            this.Snackbar.Add("Registration failed. Please try again.", Severity.Error);
        }
    }
}