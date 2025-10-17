using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProveedoresController: ControllerBase
    {
        private readonly GetAllProveedores getAllProveedores;
        private readonly GetProveedorById getProveedorById;
        private readonly PostProveedor postProveedor;
        private readonly PutProveedor putProveedor;
        private readonly DeleteProveedor deleteProveedor;
        private readonly ServicioUsuarios servicioUsuarios;

        public ProveedoresController(GetAllProveedores getAllProveedores, GetProveedorById getProveedorById, PostProveedor postProveedor,
                                    PutProveedor putProveedor, DeleteProveedor deleteProveedor, ServicioUsuarios servicioUsuarios)
        {
            this.getAllProveedores = getAllProveedores;
            this.getProveedorById = getProveedorById;
            this.postProveedor = postProveedor;
            this.putProveedor = putProveedor;
            this.deleteProveedor = deleteProveedor;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDTO>>> Get()
        {
            try
            {
                var proveedores = await getAllProveedores.ExecuteAsync();

                return Ok(proveedores);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDTO>> Get(int id)
        {
            try
            {
                var proveedor = await getProveedorById.ExecuteAsync(id);

                return Ok(proveedor);
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
        public async Task<ActionResult> Post(ProveedorCreacionDTO proveedorCreacionDTO)
        {
            try
            {
                var idUsuario = servicioUsuarios.ObtenerUsuarioId();
                if (idUsuario is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var proveedorConUserId = new ProveedorCreacionConUserIdDTO
                {
                    UserId = idUsuario,
                    Apellidos = proveedorCreacionDTO.Apellidos,
                    EstadoId = proveedorCreacionDTO.EstadoId,
                    Nit = proveedorCreacionDTO.Nit,
                    Nombres = proveedorCreacionDTO.Nombres,
                    Telefono = proveedorCreacionDTO.Telefono,
                    Ubicacion = proveedorCreacionDTO.Ubicacion
                };

                var result = await postProveedor.ExecuteAsync(proveedorConUserId);
                if (result) return Created();

                return BadRequest("Ha ocurrido un error al crear el Proveedor");
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
        public async Task<ActionResult> Put(int id, ProveedorCreacionDTO proveedorCreacionDTO)
        {
            try
            {
                var result = await putProveedor.ExecuteAsync(id, proveedorCreacionDTO);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al actualizar el Proveedor");

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
                var result = await deleteProveedor.ExecuteAsync(id);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al eliminar el Proveedor");

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
