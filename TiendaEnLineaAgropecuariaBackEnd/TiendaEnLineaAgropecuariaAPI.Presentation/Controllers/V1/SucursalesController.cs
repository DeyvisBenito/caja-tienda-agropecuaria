using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.SucursalesUseCases.SucursalesCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.SucursalesUseCases.SucursalesQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class SucursalesController: ControllerBase
    {
        private readonly GetAllSucursales getAllSucursales;
        private readonly GetSucursalById getSucursalById;
        private readonly PostSucursales postSucursales;
        private readonly PutSucursales putSucursales;
        private readonly DeleteSucursales deleteSucursales;
        private readonly ServicioUsuarios servicioUsuarios;

        public SucursalesController(GetAllSucursales getAllSucursales, GetSucursalById getSucursalById, PostSucursales postSucursales,
                                    PutSucursales putSucursales, DeleteSucursales deleteSucursales, ServicioUsuarios servicioUsuarios)
        {
            this.getAllSucursales = getAllSucursales;
            this.getSucursalById = getSucursalById;
            this.postSucursales = postSucursales;
            this.putSucursales = putSucursales;
            this.deleteSucursales = deleteSucursales;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SucursalDTO>>> Get()
        {
            try
            {
                var sucursal = await getAllSucursales.ExecuteAsync();

                return Ok(sucursal);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SucursalDTO>> Get(int id)
        {
            try
            {
                var sucursal = await getSucursalById.ExecuteAsync(id);

                return Ok(sucursal);
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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Post(SucursalCreacionDTO sucursalCreacionDTO)
        {
            try
            {
                var idUsuario = servicioUsuarios.ObtenerUsuarioId();
                if (idUsuario is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var sucursalConUserId = new SucursalCreacionConUserIdDTO
                {
                    UserId = idUsuario,
                    Nombre = sucursalCreacionDTO.Nombre,
                    EstadoId = sucursalCreacionDTO.EstadoId,
                    Ubicacion = sucursalCreacionDTO.Ubicacion
                };

                var result = await postSucursales.ExecuteAsync(sucursalConUserId);
                if (result) return Created();

                return BadRequest("Ha ocurrido un error al crear la Sucursal");
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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Put(int id, SucursalCreacionDTO sucursalCreacionDTO)
        {
            try
            {
                var result = await putSucursales.ExecuteAsync(id, sucursalCreacionDTO);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al actualizar la Sucursal");

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

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await deleteSucursales.ExecuteAsync(id);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al eliminar la Sucursal");

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
