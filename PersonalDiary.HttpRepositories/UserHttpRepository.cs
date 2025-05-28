using System.Net.Http.Json;
using PersonalDiary.Business.Models.Users;
using PersonalDiary.HttpRepositories.Contracts;

namespace PersonalDiary.HttpRepositories
{
    public class UserHttpRepository : IUserHttpRepository
    {
        private readonly HttpClient httpClient;

        public UserHttpRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<UserBiz>> GetAllAsync()
        {
            try
            {
                var response = await this.httpClient.GetFromJsonAsync<IEnumerable<UserBiz>>("api/user");

                if (response == null)
                {
                    throw new ArgumentNullException("Users not found!");
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserBiz> GetByIdAsync(long id)
        {
            try
            {
                var response = await this.httpClient.GetFromJsonAsync<UserBiz>($"api/user/{id}");

                if (response == null)
                {
                    throw new ArgumentNullException("User not found!");
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task UpdateAsync(UserBiz user)
        {
            // Ensure required fields are set
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Set modification tracking fields
            user.LastModifiedDate = DateTime.UtcNow;

            var response = await this.httpClient.PutAsJsonAsync("/api/user/update-user", user);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Update failed: {error}");
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await this.httpClient.DeleteAsync($"api/user/{id}");
            return response.IsSuccessStatusCode;
        }


        // Custom method: Register new user
        public async Task<UserBiz> RegisterUserAsync(UserBiz user)
        {
            try
            {
                user.CreationDate = DateTime.UtcNow;
                user.LastModifiedDate = DateTime.UtcNow;

                var response = await this.httpClient.PostAsJsonAsync("api/user/register", user);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Registration failed: {error}");
                }

                return await response.Content.ReadFromJsonAsync<UserBiz>();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }

        // Custom method: Update password
        public async Task<bool> ChangePasswordAsync(long userId, string newPassword)
        {
            try
            {
                var payload = new
                {
                    UserId = userId,
                    NewPassword = newPassword,
                };

                var response = await this.httpClient.PostAsJsonAsync("api/user/update-password", payload);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Sign in method
        public async Task<SignInResponseModel> SignInAsync(string username, string password)
        {
            try
            {
                var response = await this.httpClient.PostAsJsonAsync("/api/user/signin", new
                {
                    Username = username,
                    Password = password
                });

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<SignInResponseModel>();
                    return result;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return new SignInResponseModel
                    {
                        Success = false,
                        Message = error
                    };
                }
            }
            catch (Exception ex)
            {
                return new SignInResponseModel
                {
                    Success = false,
                    Message = "An error occurred while signing in."
                };
            }
        }
    }
}
