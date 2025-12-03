using System.Text;
using System.Text.Json;
using Taxas.API.DTO;

namespace Taxas.API.Servicos
{
    public class MoradorIntegrationClient
    {
        private readonly HttpClient _httpClient;

        public MoradorIntegrationClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AtualizarDivida(int idMorador, decimal valor)
        {
            var dto = new AtualizarDividaExternoDTO { ValorDaTaxa = valor };
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"api/Morador/atualizar-divida/{idMorador}", content);
        }
    }
}