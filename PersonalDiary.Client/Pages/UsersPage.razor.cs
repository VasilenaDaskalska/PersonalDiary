using Microsoft.AspNetCore.Components;
using MudBlazor;
using PersonalDiary.Business.Models.Users;
using PersonalDiary.Client.Shared.State;
using PersonalDiary.HttpRepositories.Contracts;

namespace PersonalDiary.Client.Pages
{
    public partial class UsersPage
    {
        private bool isLoading;
        private bool isSearching;
        public UserBiz SelectedUser { get; set; }
        public List<UserBiz> Users { get; set; }

        [Inject]
        private IUserHttpRepository UserHttpRepository { get; set; }

        [Inject]
        private UserState UserState { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await this.InitUserAsync();
        }

        private async Task InitUserAsync()
        {
            this.isLoading = true;
            this.Users = (await this.UserHttpRepository.GetAllAsync())?.ToList() ?? new List<UserBiz>();
            this.isLoading = false;
        }

        private async Task DeleteUser(long id)
        {
            await this.UserHttpRepository.DeleteAsync(id);
            await this.InitUserAsync();
        }
    }
}
