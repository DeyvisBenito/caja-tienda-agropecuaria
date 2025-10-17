using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.DTOs.UnidadMedidaDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.UnidadMedidaUseCases.UnidadMedidaQuerys;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UnidadesMedidaController: ControllerBase
    {
        private readonly GetAllUnidadMedida getAllUnidadMedida;

        public UnidadesMedidaController(GetAllUnidadMedida getAllUnidadMedida)
        {
            this.getAllUnidadMedida = getAllUnidadMedida;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadMedidaDTO>>> Get()
        {
            try
            {
                var unidadesMedida = await getAllUnidadMedida.ExecuteAsync();
                return Ok(unidadesMedida);
            }catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }
    }
}
