using dotnet_todo.Models;

namespace BlazorApp1.Data
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

		public async Task<List<Character>> GetAllCharacters()
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<List<Character>>("https://localhost:7163/api/Character/GetAll");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return new List<Character>();
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


	}
}
