using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using PersonalDiary.Entities;

namespace PersonalDiary.Client.Shared.State
{
    public class UserState
    {
        private readonly ILocalStorageService localStorage;
        private readonly NavigationManager navigationManager;
        private User? currentUser;

        public event Action? OnUserChanged;

        public UserState(ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            this.localStorage = localStorage;
            this.navigationManager = navigationManager;
        }

        public User? CurrentUser => this.currentUser;

        public async Task InitializeAsync()
        {
            try
            {
                var userJson = await this.localStorage.GetItemAsync<string>("user");
                if (!string.IsNullOrEmpty(userJson))
                {
                    this.currentUser = JsonSerializer.Deserialize<User>(userJson);
                    OnUserChanged?.Invoke();
                }
            }
            catch
            {
                this.currentUser = null;
            }
        }

        public async Task SetUserAsync(User? user)
        {
            this.currentUser = user;
            if (user != null)
            {
                await this.localStorage.SetItemAsync("user", JsonSerializer.Serialize(user));
            }
            else
            {
                await this.localStorage.RemoveItemAsync("user");
            }
            OnUserChanged?.Invoke();
        }

        public async Task LogoutAsync()
        {
            await this.SetUserAsync(null);
            this.navigationManager.NavigateTo("/login");
        }

        public bool IsAuthenticated => this.currentUser != null;
    }
}
