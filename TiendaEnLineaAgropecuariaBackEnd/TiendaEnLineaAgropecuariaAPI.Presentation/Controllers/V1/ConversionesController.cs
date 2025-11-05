using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ConversionesUseCases.ConversionesQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ConversionesController : ControllerBase
    {

        private readonly GetDescuentoByConversion descuentoByConversion;
        private readonly ServicioUsuarios servicioUsuarios;

        public ConversionesController(GetDescuentoByConversion descuentoByConversion, ServicioUsuarios servicioUsuarios)
        {
            this.descuentoByConversion = descuentoByConversion;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet("descuento/origen/{origenId}/destino/{destId}")]
        public async Task<ActionResult<decimal>> GetDescuento(int origenId, int destId)
        {
            try
            {
                var descuento = await descuentoByConversion.ExecuteAsync(origenId, destId);

                return Ok(descuento);
            }
            catch (KeyNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }
    }
}
