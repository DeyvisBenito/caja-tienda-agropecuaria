using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.TipoProductosDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class TiposProductoController: ControllerBase
    {
        private readonly GetAllTiposProducto getAllTiposProducto;
        private readonly GetTiposProductoById getTiposProductoById;
        private readonly PostTipoProducto postTipoProducto;
        private readonly PutTipoProducto putTipoProducto;
        private readonly DeleteTipoProducto deleteTipoProducto;
        private readonly ServicioUsuarios servicioUsuarios;

        public TiposProductoController(GetAllTiposProducto getAllTiposProducto, GetTiposProductoById getTiposProductoById, PostTipoProducto postTipoProducto,
                                    PutTipoProducto putTipoProducto, DeleteTipoProducto deleteTipoProducto, ServicioUsuarios servicioUsuarios)
        {
            this.getAllTiposProducto = getAllTiposProducto;
            this.getTiposProductoById = getTiposProductoById;
            this.postTipoProducto = postTipoProducto;
            this.putTipoProducto = putTipoProducto;
            this.deleteTipoProducto = deleteTipoProducto;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProductosDTO>>> Get()
        {
            try
            {
                var tiposProducto = await getAllTiposProducto.ExecuteAsync();

                return Ok(tiposProducto);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProductosDTO>> Get(int id)
        {
            try
            {
                var tipoProducto = await getTiposProductoById.ExecuteAsync(id);

                return Ok(tipoProducto);
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
        public async Task<ActionResult> Post(TipoProductoCreacionDTO tipoProductoCreacionDTO)
        {
            try
            {
                var idUsuario = servicioUsuarios.ObtenerUsuarioId();
                if (idUsuario is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var tipoProductoConUserId = new TipoProductoCreacionConUserIdDTO
                {
                    IdUser = idUsuario,
                    Nombre = tipoProductoCreacionDTO.Nombre,
                    CategoriaId = tipoProductoCreacionDTO.CategoriaId,
                    EstadoId = tipoProductoCreacionDTO.EstadoId
                };

                var result = await postTipoProducto.ExecuteAsync(tipoProductoConUserId);
                if (result) return Created();

                return BadRequest("Ha ocurrido un error al crear el Tipo de Producto");
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
        public async Task<ActionResult> Put(int id, TipoProductoCreacionDTO tipoProductoDTO)
        {
            try
            {
                var result = await putTipoProducto.ExecuteAsync(id, tipoProductoDTO);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al actualizar el Tipo Producto");

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
                var result = await deleteTipoProducto.ExecuteAsync(id);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al eliminar el Tipo Producto");

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
