using System.Text.Json;
using Residencias.API.DTO;

namespace Residencias.API.Servicos
{
    public class MoradorClient
    {
        private readonly HttpClient _httpClient;

        public MoradorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MoradorExternoDTO> BuscarMoradorPorId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Morador/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<MoradorExternoDTO>(json, opcoes);
                }

                return null; 
            }
            catch
            {
                return null; 
            }
        }
    }
}