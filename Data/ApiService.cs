using dotnet_todo.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net.Http.Headers;

namespace BlazorApp1.Data
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Character>?> GetAllCharacters()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Character>>("https://localhost:7163/api/Character/GetAll");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }
        public async Task<Character?> GetCharacter(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Character>($"https://localhost:7163/api/Character/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task DeleteCharacter(int id, string JWT)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://localhost:7163/api/Character/delete/{id}"),
                Method = HttpMethod.Delete,
            };
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWT);
            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
        public async Task<int> AddCharacter(Character newCharacter)
        {
            var response = await _httpClient.PostAsJsonAsync<Character>("https://localhost:7163/api/Character/new", newCharacter);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
            string character_id_str = await response.Content.ReadAsStringAsync();
            int character_id = Convert.ToInt32(character_id_str);
            return character_id;
        }
        public async Task UpdateCharacter(UpdateCharacterDTO character)
        {
            var response = await _httpClient.PatchAsJsonAsync("https://localhost:7163/api/Character/update", character);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
    }
}
