namespace Residencias.API.Servicos
{
    public class Residencia
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Bloco { get; set; }
        public bool Ativa { get; set; } 
        public int IdMoradorResponsavel { get; set; }
    }
}