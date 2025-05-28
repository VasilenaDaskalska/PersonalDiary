using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PersonalDiary.Business.Models.Users;

namespace PersonalDiary.Client.Shared.Dialogs
{
    public partial class ChangePasswordDialog
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        private MudForm form;
        private bool success;

        private string CurrentPassword { get; set; }
        private string NewPassword { get; set; }
        private string ConfirmPassword { get; set; }

        // Current password visibility
        private bool currentPasswordVisible;
        private InputType currentPasswordInput = InputType.Password;
        private string currentPasswordIcon = Icons.Material.Filled.VisibilityOff;

        // New password visibility
        private bool newPasswordVisible;
        private InputType newPasswordInput = InputType.Password;
        private string newPasswordIcon = Icons.Material.Filled.VisibilityOff;

        // Confirm password visibility
        private bool confirmPasswordVisible;
        private InputType confirmPasswordInput = InputType.Password;
        private string confirmPasswordIcon = Icons.Material.Filled.VisibilityOff;

        protected Func<string, Task<IEnumerable<string>>> ValidateRepeatPassword => (p) =>
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(this.NewPassword) ||
                string.IsNullOrWhiteSpace(p) ||
                !this.NewPassword.Equals(p))
            {
                result.Add("Password doesnt match!");

            }

            return Task.FromResult<IEnumerable<string>>(result);
        };

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

        private void TogglePasswordVisibility(ref bool visible, ref InputType inputType, ref string icon)
        {
            if (visible)
            {
                visible = false;
                inputType = InputType.Password;
                icon = Icons.Material.Filled.VisibilityOff;
            }
            else
            {
                visible = true;
                inputType = InputType.Text;
                icon = Icons.Material.Filled.Visibility;
            }
        }

        private async Task ClearFormAsync()
        {
            this.CurrentPassword = null;
            this.NewPassword = null;
            this.ConfirmPassword = null;
            await this.form.ResetAsync();
        }

        private async Task Submit()
        {
            await this.form.Validate();

            if (this.form.IsValid)
            {
                if (this.NewPassword != this.ConfirmPassword)
                {
                    return;
                }

                var result = new PasswordChangeModel
                {
                    CurrentPassword = this.CurrentPassword,
                    NewPassword = this.NewPassword
                };

                this.MudDialog.Close(DialogResult.Ok(result));
            }
        }

        private async Task CancelAsync()
        {
            this.MudDialog.Cancel();
        }
    }
}
