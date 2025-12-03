namespace Taxas.API.DTO
{
    public class ExibirTaxaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public int NumeroResidencia { get; set; }
        public string BlocoResidencia { get; set; }
    }
}