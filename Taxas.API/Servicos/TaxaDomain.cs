using Taxas.API.DTO;

namespace Taxas.API.Servicos
{
    public class TaxaDomain
    {
        private readonly DataContext _dataContext;
        private readonly ResidenciaClient _residenciaClient;
        private readonly MoradorIntegrationClient _moradorClient;

        public TaxaDomain(DataContext dataContext,
                          ResidenciaClient residenciaClient,
                          MoradorIntegrationClient moradorClient)
        {
            _dataContext = dataContext;
            _residenciaClient = residenciaClient;
            _moradorClient = moradorClient;
        }

        public async Task LancarTaxa(LancarTaxaDTO dto)
        {

            var residencia = await _residenciaClient.BuscarResidencia(dto.IdResidencia);

            if (residencia == null)
                throw new Exception("Residência não encontrada. Não é possível lançar taxa.");

            if (!residencia.Ativa)
                throw new Exception("Residência inativa. Não é possível lançar taxa.");

            var novaTaxa = new TaxaCondominio
            {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                IdResidencia = dto.IdResidencia,
                DataVencimento = DateTime.Now.AddDays(30)
            };

            _dataContext.Taxas.Add(novaTaxa);
            await _dataContext.SaveChangesAsync();

            await _moradorClient.AtualizarDivida(residencia.IdMoradorResponsavel, dto.Valor);
        }

        public async Task<ExibirTaxaDTO> BuscarTaxaPorId(int id)
        {
            var taxa = _dataContext.Taxas.FirstOrDefault(t => t.Id == id);
            if (taxa == null) return null;

            var residencia = await _residenciaClient.BuscarResidencia(taxa.IdResidencia);

            return new ExibirTaxaDTO
            {
                Id = taxa.Id,
                Descricao = taxa.Descricao,
                Valor = taxa.Valor,
                DataVencimento = taxa.DataVencimento,

                NumeroResidencia = residencia != null ? residencia.Numero : 0,
                BlocoResidencia = residencia != null ? residencia.Bloco : "N/A"
            };
        }
    }

}