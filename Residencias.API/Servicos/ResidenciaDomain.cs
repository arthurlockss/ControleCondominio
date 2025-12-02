using Residencias.API.DTO;

namespace Residencias.API.Servicos
{
    public class ResidenciaDomain
    {
        private readonly DataContext _dataContext;
        private readonly MoradorClient _moradorClient;

        public ResidenciaDomain(DataContext dataContext, MoradorClient moradorClient)
        {
            _dataContext = dataContext;
            _moradorClient = moradorClient;
        }

        public async Task Cadastrar(InserirResidenciaDTO dto)
        {
            var morador = await _moradorClient.BuscarMoradorPorId(dto.IdMoradorResponsavel);

            if (morador == null)
            {
                throw new Exception("Morador não encontrado. Cadastro cancelado.");
            }

            if (morador.ValorDivida > 0)
            {
                throw new Exception($"Cadastro negado: O morador {morador.Nome} possui dívida de R$ {morador.ValorDivida}.");
            }

            var novaResidencia = new Residencia
            {
                Numero = dto.Numero,
                Bloco = dto.Bloco,
                Ativa = true,
                IdMoradorResponsavel = dto.IdMoradorResponsavel
            };

            _dataContext.Residencias.Add(novaResidencia);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ExibirResidenciaDTO> BuscarPorId(int id)
        {
            var residencia = _dataContext.Residencias.FirstOrDefault(r => r.Id == id);

            if (residencia == null) return null;

            var morador = await _moradorClient.BuscarMoradorPorId(residencia.IdMoradorResponsavel);

            var resultado = new ExibirResidenciaDTO
            {
                Id = residencia.Id,
                Numero = residencia.Numero,
                Bloco = residencia.Bloco,
                Ativa = residencia.Ativa,
                IdMoradorResponsavel = residencia.IdMoradorResponsavel,
                NomeMorador = morador != null ? morador.Nome : "Morador não encontrado",
                TelefoneMorador = morador != null ? morador.Telefone : "N/A"
            };

            return resultado;
        }
    }
}