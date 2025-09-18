using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CategoriasController : ControllerBase
    {
        private readonly GetAllCategorias getAllCategorias;
        private readonly GetCategoriaById getCategoriaById;
        private readonly PostCategoria postCategoria;
        private readonly PutCategorias putCategorias;
        private readonly DeleteCategorias deleteCategorias;
        private readonly ServicioUsuarios servicioUsuarios;

        public CategoriasController(GetAllCategorias getAllCategorias, GetCategoriaById getCategoriaById, PostCategoria postCategoria,
                                    PutCategorias putCategorias, DeleteCategorias deleteCategorias, ServicioUsuarios servicioUsuarios)
        {
            this.getAllCategorias = getAllCategorias;
            this.getCategoriaById = getCategoriaById;
            this.postCategoria = postCategoria;
            this.putCategorias = putCategorias;
            this.deleteCategorias = deleteCategorias;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            try
            {
                var categorias = await getAllCategorias.ExecuteAsync();

                return Ok(categorias);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            try
            {
                var categoria = await getCategoriaById.ExecuteAsync(id);

                return Ok(categoria);
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
        public async Task<ActionResult> Post(CategoriaCreacionDTO categoriaCreacionDTO)
        {
            try
            {
                var idUsuario = servicioUsuarios.ObtenerUsuarioId();
                if (idUsuario is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var categoriaConUserId = new CategoriaCreacionConUserIdDTO
                {
                    IdEstado = categoriaCreacionDTO.IdEstado,
                    Nombre = categoriaCreacionDTO.Nombre,
                    UserId = idUsuario
                };

                var result = await postCategoria.ExecuteAsync(categoriaConUserId);
                if (result) return Created();

                return BadRequest("Ha ocurrido un error al crear la categoria");
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
        public async Task<ActionResult> Put(int id, CategoriaCreacionDTO categoriaCreacionDTO)
        {
            try
            {
                var result = await putCategorias.ExecuteAsync(id, categoriaCreacionDTO);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al actualizar la categoria");

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
                var result = await deleteCategorias.ExecuteAsync(id);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al eliminar la categoria");

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
