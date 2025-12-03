namespace Taxas.API.Servicos
{
    public class TaxaCondominio
    {
        public int Id { get; set; }
        public string Descricao { get; set; } 
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }

        public int IdResidencia { get; set; }
    }
}