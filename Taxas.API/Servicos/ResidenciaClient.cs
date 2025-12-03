using System.Text.Json;
using Taxas.API.DTO;

namespace Taxas.API.Servicos
{
    public class ResidenciaClient
    {
        private readonly HttpClient _httpClient;

        public ResidenciaClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResidenciaExternaDTO> BuscarResidencia(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Residencia/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<ResidenciaExternaDTO>(json, opcoes);
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