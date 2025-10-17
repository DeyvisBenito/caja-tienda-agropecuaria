using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.CarritoDetDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.CarritoDetUseCases.CarritoDetCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CarritoDetUseCases.CarritoDetQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CarritoDetalleController : ControllerBase
    {
        /*private readonly GetAllCarritoDet getAllCarritoDet;
        private readonly GetCarritoDetById getCarritoDetById;
        private readonly PostCarritoDet postCarritoDet;
        private readonly PutCarritoDet putCarritoDet;
        private readonly DeleteCarritoDet deleteCarritoDet;
        private readonly ServicioUsuarios servicioUsuarios;
        private readonly ServicioInventarios servicioInventarios;

        public CarritoDetalleController(GetAllCarritoDet getAllCarritoDet, GetCarritoDetById getCarritoDetById, PostCarritoDet postCarritoDet,
                                    PutCarritoDet putCarritoDet, DeleteCarritoDet deleteCarritoDet, ServicioUsuarios servicioUsuarios,
                                    ServicioInventarios servicioInventarios)
        {
            this.getAllCarritoDet = getAllCarritoDet;
            this.getCarritoDetById = getCarritoDetById;
            this.postCarritoDet = postCarritoDet;
            this.putCarritoDet = putCarritoDet;
            this.deleteCarritoDet = deleteCarritoDet;
            this.servicioUsuarios = servicioUsuarios;
            this.servicioInventarios = servicioInventarios;
        }*/

       /* [HttpGet]
        public async Task<ActionResult<IEnumerable<CarritoDetDTO>>> Get()
        {
            try
            {
                var userId = servicioUsuarios.ObtenerUsuarioId();
                if(userId is null)
                {
                    return BadRequest("El Usuario no esta logueado");
                }
                var carritoDets = await getAllCarritoDet.ExecuteAsync(userId);
                foreach (var carritoDet in carritoDets)
                {
                    var stockDisp = await servicioInventarios.ObtenerStock(carritoDet.InventarioId);
                    carritoDet.HasConflict = carritoDet.Cantidad > stockDisp;
                }

                return Ok(carritoDets);
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

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDetDTO>> Get(int id)
        {
            try
            {
                var userId = servicioUsuarios.ObtenerUsuarioId();
                if(userId is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var carritoDet = await getCarritoDetById.ExecuteAsync(id, userId);
                var stockDisp = await servicioInventarios.ObtenerStock(carritoDet.InventarioId);
                carritoDet.HasConflict = carritoDet.Cantidad > stockDisp;

                return Ok(carritoDet);
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

        [HttpPost]
        public async Task<ActionResult> Post(CarritoDetCreacionDTO carritoDetCreacionDTO)
        {
            try
            {
                var userId = servicioUsuarios.ObtenerUsuarioId();
                if (userId is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var result = await postCarritoDet.ExecuteAsync(userId, carritoDetCreacionDTO);
                if (result) return Created();

                return BadRequest("Ha ocurrido un error al agregar el producto al carrito");
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CarritoDetCreacionDTO carritoDetCreacionDTO)
        {
            try
            {
                var userId = servicioUsuarios.ObtenerUsuarioId();
                if(userId is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var result = await putCarritoDet.ExecuteAsync(userId, id, carritoDetCreacionDTO);
                if (result) return Ok(new {success = true});

                return BadRequest("Ha ocurrido un error al actualizar el producto del carrito");

            }
            catch (KeyNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
            catch (InvalidOperationException e)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var userId = servicioUsuarios.ObtenerUsuarioId();
                if (userId is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var result = await deleteCarritoDet.ExecuteAsync(userId, id);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al eliminar el producto del carrito");

            }
            catch (KeyNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        } */
    }
}
