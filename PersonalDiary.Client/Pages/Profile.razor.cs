using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PersonalDiary.Business.Models.Users;
using PersonalDiary.Client.Shared.Dialogs;
using PersonalDiary.Client.Shared.State;
using PersonalDiary.HttpRepositories.Contracts;
using PersonalDiary.Mappers;

namespace PersonalDiary.Client.Pages
{
    public partial class Profile
    {
        private MudForm form;
        private bool success;

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        private IUserHttpRepository UserHttpRepository { get; set; }

        [Inject]
        private UserState UserState { get; set; }

        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        private UserBiz CurrentUser { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.CurrentUser = await this.UserHttpRepository.GetByIdAsync(this.UserState.CurrentUser.ID);

            if (this.CurrentUser == null)
            {
                return;
            }
        }

        private async Task ClearFormAsync()
        {
            await this.OnInitializedAsync();
            await this.form.ResetAsync();
        }

        private async Task ChangePasswordAsync()
        {
            var dialog = await this.DialogService.ShowAsync<ChangePasswordDialog>("Change Password");
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var passwordChange = (PasswordChangeModel)result.Data;
                try
                {
                    // Here you might want to add an endpoint to verify current password before changing
                    await this.UserHttpRepository.ChangePasswordAsync(this.CurrentUser.ID, passwordChange.NewPassword);
                    this.CurrentUser.Password = passwordChange.NewPassword;
                    this.Snackbar.Add("Password changed successfully!", Severity.Success);
                }
                catch (Exception ex)
                {
                    this.Snackbar.Add("Failed to change password. Please try again.", Severity.Error);
                }
            }
        }

        private async Task SaveChangesAsync()
        {
            await this.form.Validate();

            if (this.form.IsValid)
            {
                try
                {
                    await this.UserHttpRepository.UpdateAsync(this.CurrentUser);
                    var sessionUser = UserMapper.MapUserEntityFromUserBiz(this.CurrentUser);
                    await this.UserState.SetUserAsync(sessionUser);
                    await this.LocalStorage.SetItemAsync("user", this.CurrentUser);

                    this.Snackbar.Add("Profile updated successfully!", Severity.Success);
                }
                catch (Exception ex)
                {
                    this.Snackbar.Add("Failed to update profile. Please try again.", Severity.Error);
                }
            }
        }
    }
}
