using BlazorApp1.Models;

namespace BlazorApp1.Data
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

		public async Task<List<GetCharacterDTO>> GetAllCharacters()
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<List<GetCharacterDTO>>("https://localhost:7163/api/Character/GetAll");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return new List<GetCharacterDTO>();
			}
		}

	}
}
