using Microsoft.AspNetCore.Mvc;
using Taxas.API.DTO;
using Taxas.API.Servicos;

namespace Taxas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxaController : ControllerBase
    {
        private readonly TaxaDomain _taxaDomain;

        public TaxaController(TaxaDomain taxaDomain)
        {
            _taxaDomain = taxaDomain;
        }

        [HttpPost]
        public async Task<IActionResult> LancarTaxa([FromBody] LancarTaxaDTO dto)
        {
            try
            {
                await _taxaDomain.LancarTaxa(dto);
                return Ok(new { Mensagem = "Taxa lançada e dívida do morador atualizada!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var taxa = await _taxaDomain.BuscarTaxaPorId(id);
            if (taxa == null) return NotFound("Taxa não encontrada.");

            return Ok(taxa);
        }
    }
}