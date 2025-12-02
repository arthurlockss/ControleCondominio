namespace Moradores.API.Servicos
{
    public class Morador
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }

        public decimal ValorDivida { get; set; }
    }
}