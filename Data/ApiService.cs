using dotnet_todo.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BlazorApp1.Data
{
    public class ApiService
    {
        private string _accessToken;
        private string _refreshToken;
        public class MyCustomContractResolver : JsonNamingPolicy
        {
            public override string ConvertName(string name)
            {
                return name.Contains("ash") ? "password" : name;
            }
        }
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<bool> Register(IdentityUser user)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new MyCustomContractResolver(),
            };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7163/register", user, options);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Login(IdentityUser user)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new MyCustomContractResolver(),
            };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7163/login", user, options);
            if (!response.IsSuccessStatusCode) 
            {
                throw new Exception();
            }
            var response_string = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(response_string);
            _accessToken = (string)jsonObject["accessToken"];
            _refreshToken = (string)jsonObject["refreshToken"];
            return true;
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
        public async Task DeleteCharacter(int id)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://localhost:7163/api/Character/delete/{id}"),
                Method = HttpMethod.Delete,
            };
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            //Console.WriteLine(httpRequestMessage);
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
