using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.UseCases.BodegasUseCases.BodegasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.BodegasUseCases.BodegasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class BodegasController: ControllerBase
    {
        private readonly GetAllBodegas getAllBodegas;
        private readonly GetBodegaById getBodegaById;
        private readonly PostBodegas postBodegas;
        private readonly PutBodegas putBodegas;
        private readonly DeleteBodegas deleteBodegas;
        private readonly ServicioUsuarios servicioUsuarios;

        public BodegasController(GetAllBodegas getAllBodegas, GetBodegaById getBodegaById, PostBodegas postBodegas,
                                    PutBodegas putBodegas, DeleteBodegas deleteBodegas, ServicioUsuarios servicioUsuarios)
        {
            this.getAllBodegas = getAllBodegas;
            this.getBodegaById = getBodegaById;
            this.postBodegas = postBodegas;
            this.putBodegas = putBodegas;
            this.deleteBodegas = deleteBodegas;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BodegaDTO>>> Get()
        {
            try
            {
                var bodegas = await getAllBodegas.ExecuteAsync();

                return Ok(bodegas);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BodegaDTO>> Get(int id)
        {
            try
            {
                var bodega = await getBodegaById.ExecuteAsync(id);

                return Ok(bodega);
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
        public async Task<ActionResult> Post(BodegaCreacionDTO bodegaCreacionDTO)
        {
            try
            {
                var idUsuario = servicioUsuarios.ObtenerUsuarioId();
                if (idUsuario is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var bodegaConUserId = new BodegaCreacionConUserIdDTO
                {
                    UserId = idUsuario,
                    Nombre = bodegaCreacionDTO.Nombre,
                    EstadoId = bodegaCreacionDTO.EstadoId,
                    Ubicacion = bodegaCreacionDTO.Ubicacion
                };

                var result = await postBodegas.ExecuteAsync(bodegaConUserId);
                if (result) return Created();

                return BadRequest("Ha ocurrido un error al crear la Bodega");
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
        public async Task<ActionResult> Put(int id, BodegaCreacionDTO bodegaCreacionDTO)
        {
            try
            {
                var result = await putBodegas.ExecuteAsync(id, bodegaCreacionDTO);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al actualizar la Bodega");

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
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await deleteBodegas.ExecuteAsync(id);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al eliminar la Bodega");

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
