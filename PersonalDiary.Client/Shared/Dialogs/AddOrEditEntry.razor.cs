using Microsoft.AspNetCore.Components;
using MudBlazor;
using PersonalDiary.Business.Models.Diaries;
using PersonalDiary.Client.Shared.State;
using PersonalDiary.HttpRepositories.Contracts;

namespace PersonalDiary.Client.Shared.Dialogs
{
    public partial class AddOrEditEntry
    {
        private MudForm form;
        private bool success;

        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Parameter]
        public DiaryBiz Entry { get; set; }

        [Inject]
        private IDiaryHttpRepository DiaryHttpRepository { get; set; }

        [Inject]
        private UserState UserState { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async Task ClearFormAsync()
        {
            await this.form.ResetAsync();
        }

        private async Task SaveAsync()
        {
            await this.form.Validate();

            if (this.form.IsValid && this.Entry != null)
            {
                var now = DateTime.UtcNow;
                this.Entry.LastModifiedDate = now;
                this.Entry.LastUserModifiedID = this.UserState.CurrentUser?.ID ?? 0;

                if (this.Entry.ID > 0)
                {
                    try
                    {
                        await this.DiaryHttpRepository.UpdateAsync(this.Entry);
                        this.Snackbar.Add("Entry updated successfully!", Severity.Success);
                    }
                    catch (Exception ex)
                    {
                        this.Snackbar.Add("Failed to update entry. Please try again.", Severity.Error);
                        return;
                    }
                }
                else
                {
                    // Set required fields for new entry
                    this.Entry.CreationDate = now;
                    this.Entry.UserCreatorID = this.UserState.CurrentUser?.ID ?? 0;
                    this.Entry.IsDeleted = false;

                    try
                    {
                        var response = await this.DiaryHttpRepository.CreateAsync(this.Entry);
                        if (response != null)
                        {
                            this.Entry = response;
                            this.Snackbar.Add("Entry created successfully!", Severity.Success);
                        }
                        else
                        {
                            this.Snackbar.Add("Failed to create entry. Please try again.", Severity.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Snackbar.Add($"Failed to create entry: {ex.Message}", Severity.Error);
                        return;
                    }
                }

                this.MudDialog.Close(DialogResult.Ok(this.Entry));
            }
        }

        private async Task CancelAsync()
        {
            this.MudDialog.Cancel();
        }
    }
}
