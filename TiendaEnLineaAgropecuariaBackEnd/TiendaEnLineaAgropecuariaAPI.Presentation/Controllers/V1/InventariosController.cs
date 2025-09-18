using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.InventariosUseCases.InventariosCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.InventariosUseCases.InventariosQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class InventariosController : ControllerBase
    {
        private readonly GetAllInventarios getAllInventarios;
        private readonly GetInventarioById getInventarioById;
        private readonly PostInventario postInventario;
        private readonly PutInventario putInventario;
        private readonly DeleteInventario deleteInventario;
        private readonly ServicioUsuarios servicioUsuarios;
        private readonly ApplicationDBContext dbContext;
        private const string contenedor = "inventarios";
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public InventariosController(IAlmacenadorArchivos almacenadorArchivos, GetAllInventarios getAllInventarios,
                                    GetInventarioById getInventarioById, PostInventario postInventario,
                                    PutInventario putInventario, DeleteInventario deleteInventario, ServicioUsuarios servicioUsuarios,
                                    ApplicationDBContext dbContext)
        {
            this.almacenadorArchivos = almacenadorArchivos;
            this.getAllInventarios = getAllInventarios;
            this.getInventarioById = getInventarioById;
            this.postInventario = postInventario;
            this.putInventario = putInventario;
            this.deleteInventario = deleteInventario;
            this.servicioUsuarios = servicioUsuarios;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioDTO>>> Get()
        {
            try
            {
                var inventarios = await getAllInventarios.ExecuteAsync();

                return Ok(inventarios);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventarioDTO>> Get(int id)
        {
            try
            {
                var inventario = await getInventarioById.ExecuteAsync(id);

                return Ok(inventario);
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
        public async Task<ActionResult> Post([FromForm] InventarioCreacionDTO inventarioCreacionDTO)
        {
            try
            {
                if (inventarioCreacionDTO.Stock < 0)
                {
                    return BadRequest("No puede agregar un producto al inventario con stock negativo");
                }
                if (inventarioCreacionDTO.Precio < 0)
                {
                    return BadRequest("No puede agregar un producto al inventario con precio negativo");
                }
                var idUsuario = servicioUsuarios.ObtenerUsuarioId();
                if (idUsuario is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var url = await almacenadorArchivos.Almacenar(contenedor, inventarioCreacionDTO.Foto!);

                var inventarioConUserId = new InventarioCreacionConUserIdDTO
                {
                    IdUser = idUsuario,
                    UrlFoto = url,
                    Descripcion = inventarioCreacionDTO.Descripcion,
                    Marca = inventarioCreacionDTO.Marca,
                    Nombre = inventarioCreacionDTO.Nombre,
                    BodegaId = inventarioCreacionDTO.BodegaId,
                    EstadoId = inventarioCreacionDTO.EstadoId,
                    Precio = inventarioCreacionDTO.Precio,
                    Stock = inventarioCreacionDTO.Stock,
                    TipoProductoId = inventarioCreacionDTO.TipoProductoId
                };

                var result = await postInventario.ExecuteAsync(inventarioConUserId);
                if (result) return Created("", new { success = true });

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
        public async Task<ActionResult> Put(int id, [FromForm] InventarioUpdateDTO inventarioUpdate)
        {
            try
            {
                if (inventarioUpdate.Stock < 0)
                {
                    return BadRequest("No puede modificar el producto del inventario con stock negativo");
                }
                if (inventarioUpdate.Precio < 0)
                {
                    return BadRequest("No puede modificar el producto del inventario con precio negativo");
                }
                var urlFotoActual = await dbContext.Inventarios.Where(x => x.Id == id).Select(x => x.UrlFoto).FirstOrDefaultAsync();
                if (urlFotoActual is null)
                {
                    return BadRequest("Error al actualizar foto");
                }

                var inventarioCreacionDTO = new InventarioCreacionDTO
                {
                    Nombre = inventarioUpdate.Nombre,
                    Marca = inventarioUpdate.Marca,
                    BodegaId = inventarioUpdate.BodegaId,
                    Descripcion = inventarioUpdate.Descripcion,
                    EstadoId = inventarioUpdate.EstadoId,
                    Precio = inventarioUpdate.Precio,
                    Stock = inventarioUpdate.Stock,
                    TipoProductoId = inventarioUpdate.TipoProductoId
                };

                if (inventarioUpdate.Foto is null)
                {

                    var resul = await putInventario.ExecuteAsync(id, inventarioCreacionDTO, urlFotoActual);
                    if (resul) return Ok(new { success = true });
                    return BadRequest("Ha ocurrido un error al actualizar el producto en el inventario");
                }

                var urlFoto = await almacenadorArchivos.Editar(urlFotoActual, contenedor, inventarioUpdate.Foto!);

                var result = await putInventario.ExecuteAsync(id, inventarioCreacionDTO, urlFoto);
                if (result) return Ok(new { success = true });

                return BadRequest("Ha ocurrido un error al actualizar el producto en el inventario");

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
                var inventario = await dbContext.Inventarios.FirstOrDefaultAsync(x => x.Id == id);
                if (inventario is null)
                {
                    return NotFound("El inventario a eliminar no existe");
                }

                var result = await deleteInventario.ExecuteAsync(id);


                if (result)
                {
                    await almacenadorArchivos.Borrar(inventario.UrlFoto, contenedor);
                    return Ok(new { success = true });
                }

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
