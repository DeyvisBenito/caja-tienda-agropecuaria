using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.UseCases.CarritosUseCases.CarritosQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CarritoController : ControllerBase
    {
        private readonly GetCarritoByUserId getCarritoByUser;
        private readonly ServicioUsuarios servicioUsuarios;

        public CarritoController(GetCarritoByUserId getCarritoByUser, ServicioUsuarios servicioUsuarios)
        {
            this.getCarritoByUser = getCarritoByUser;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var userId = servicioUsuarios.ObtenerUsuarioId();
                if(userId is null)
                {
                    return BadRequest("Usuario no logueado");
                }

                var carrito = await getCarritoByUser.ExecuteAsync(userId);
                if (carrito is null)
                {
                    return BadRequest("Ha ocurrido un error");
                }

                return Ok(carrito);

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
