namespace Residencias.API.DTO
{
    public class ExibirResidenciaDTO
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Bloco { get; set; }
        public bool Ativa { get; set; }

        public int IdMoradorResponsavel { get; set; }

        public string NomeMorador { get; set; }
        public string TelefoneMorador { get; set; }
    }
}