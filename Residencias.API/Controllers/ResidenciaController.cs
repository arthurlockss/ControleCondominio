using Microsoft.AspNetCore.Mvc;
using Residencias.API.DTO;
using Residencias.API.Servicos;

namespace Residencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciaController : ControllerBase
    {
        private readonly ResidenciaDomain _residenciaDomain;

        public ResidenciaController(ResidenciaDomain residenciaDomain)
        {
            _residenciaDomain = residenciaDomain;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] InserirResidenciaDTO dto)
        {
            try
            {
                await _residenciaDomain.Cadastrar(dto);
                return Ok(new { Mensagem = "Residência cadastrada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var residencia = await _residenciaDomain.BuscarPorId(id);
            if (residencia == null) return NotFound("Residência não encontrada.");

            return Ok(residencia);
        }
    }
}