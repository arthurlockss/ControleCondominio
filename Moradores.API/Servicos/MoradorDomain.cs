using Moradores.API.DTO;
using System.Linq;

namespace Moradores.API.Servicos
{
    public class MoradorDomain
    {
        private readonly DataContext _dataContext;

        public MoradorDomain(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Cadastrar(InserirMoradorDTO dto)
        {
            var novoMorador = new Morador
            {
                Nome = dto.Nome,
                Cpf = dto.Cpf,
                Telefone = dto.Telefone,
                ValorDivida = 0 
            };

            _dataContext.Moradores.Add(novoMorador);
            _dataContext.SaveChanges();
        }

        public Morador BuscarPorId(int id)
        {
            return _dataContext.Moradores.FirstOrDefault(m => m.Id == id);
        }

        public void AdicionarDivida(int id, decimal valor)
        {
            var morador = _dataContext.Moradores.FirstOrDefault(m => m.Id == id);
            if (morador != null)
            {
                morador.ValorDivida += valor;
                _dataContext.SaveChanges();
            }
        }
    }
}