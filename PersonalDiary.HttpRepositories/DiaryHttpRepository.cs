using System.Net;
using System.Net.Http.Json;
using PersonalDiary.Business.Models.Diaries;
using PersonalDiary.HttpRepositories.Contracts;

namespace PersonalDiary.HttpRepositories
{
    public class DiaryHttpRepository : IDiaryHttpRepository
    {
        private readonly HttpClient httpClient;

        public DiaryHttpRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<DiaryBiz> CreateAsync(DiaryBiz model)
        {
            var response = await this.httpClient.PostAsJsonAsync("api/diary", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DiaryBiz>();
        }

        public async Task<IEnumerable<DiaryBiz>> GetAllAsync()
        {
            try
            {
                var response = await this.httpClient.GetFromJsonAsync<IEnumerable<DiaryBiz>>("api/diary");
                return response;
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    // Handle 404 specifically
                    Console.WriteLine("Error: Endpoint not found.");
                }
                else
                {
                    // Handle other types of exceptions
                    Console.WriteLine($"Request failed: {ex.Message}");
                }
                return Enumerable.Empty<DiaryBiz>();  // Return an empty list as fallback
            }
        }

        public async Task<DiaryBiz?> GetByIdAsync(long id)
            => await this.httpClient.GetFromJsonAsync<DiaryBiz>($"api/diary/{id}");

        public async Task UpdateAsync(DiaryBiz diary)
        {
            var response = await this.httpClient.PutAsJsonAsync($"api/diary", diary);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await this.httpClient.DeleteAsync($"api/diary/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
