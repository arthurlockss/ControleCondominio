namespace Taxas.API.DTO
{
    public class ResidenciaExternaDTO
    {
        public int Id { get; set; }
        public bool Ativa { get; set; }
        public int IdMoradorResponsavel { get; set; }
        public int Numero { get; set; }
        public string Bloco { get; set; }
    }
}