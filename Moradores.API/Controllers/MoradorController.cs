using Microsoft.AspNetCore.Mvc;
using Moradores.API.DTO;
using Moradores.API.Servicos;

namespace Moradores.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoradorController : ControllerBase
    {
        private readonly MoradorDomain _moradorDomain;

        public MoradorController(MoradorDomain moradorDomain)
        {
            _moradorDomain = moradorDomain;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] InserirMoradorDTO dto)
        {
            _moradorDomain.Cadastrar(dto);
            return Ok(new { Mensagem = "Morador cadastrado com sucesso!" });
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            var morador = _moradorDomain.BuscarPorId(id);
            if (morador == null) return NotFound("Morador não encontrado.");
            return Ok(morador);
        }

        [HttpPut("atualizar-divida/{id}")]
        public IActionResult AtualizarDivida(int id, [FromBody] AtualizarDividaDTO dto)
        {
            _moradorDomain.AdicionarDivida(id, dto.ValorDaTaxa);
            return Ok(new { Mensagem = "Dívida atualizada com sucesso." });
        }
    }
}