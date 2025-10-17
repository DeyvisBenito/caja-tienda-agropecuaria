using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgropecuaria.Application.UseCases.TiposMedidaUseCases.TiposMedidaQuerys;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Authorize]
    public class TiposMedidaController: ControllerBase
    {
        private readonly GetAllTiposMedida getAllTiposMedida;

        public TiposMedidaController(GetAllTiposMedida getAllTiposMedida)
        {
            this.getAllTiposMedida = getAllTiposMedida;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMedida>>> Get()
        {
            try
            {
                var tiposMedida = await getAllTiposMedida.ExecuteAsync();

                return Ok(tiposMedida);
            }catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }
    }
}
