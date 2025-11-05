using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesVentaDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetCompraUseCases.DetCompraCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetCompraUseCases.DetCompraQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetVentaUseCases.DetVentaCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetVentaUseCases.DetVentaQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class DetalleVentaController : ControllerBase
    {
        private readonly GetAllDetVentas getAllDetVentas;
        private readonly GetAllDetVentaByVentaId getAllDetVentaByVentaId;
        private readonly GetDetVentaById getDetVentaById;
        private readonly GetDetVentaByInvId getDetVentaByInvId;
        private readonly GetDetVentaByInvIdUpd getDetVentaByInvIdUpd;
        private readonly PostDetVenta postDetVenta;
        private readonly PutDetVenta putDetVenta;
        private readonly DeleteDetVenta deleteDetVenta;
        private readonly ServicioUsuarios servicioUsuarios;

        public DetalleVentaController(GetAllDetVentas getAllDetVentas, GetAllDetVentaByVentaId getAllDetVentaByVentaId,
                                        GetDetVentaById getDetVentaById, PutDetVenta putDetVenta, DeleteDetVenta deleteDetVenta,
                                        PostDetVenta postDetVenta, GetDetVentaByInvId getDetVentaByInvId, ServicioUsuarios servicioUsuarios,
                                        GetDetVentaByInvIdUpd getDetVentaByInvIdUpd)
        {
            this.getAllDetVentas = getAllDetVentas;
            this.getAllDetVentaByVentaId = getAllDetVentaByVentaId;
            this.getDetVentaById = getDetVentaById;
            this.postDetVenta = postDetVenta;
            this.getDetVentaByInvId = getDetVentaByInvId;
            this.putDetVenta = putDetVenta;
            this.deleteDetVenta = deleteDetVenta;
            this.servicioUsuarios = servicioUsuarios;
            this.getDetVentaByInvIdUpd = getDetVentaByInvIdUpd;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVentaDTO>>> Get()
        {
            try
            {
                var detsVenta = await getAllDetVentas.ExecuteAsync();

                return Ok(detsVenta);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("byventa/{ventaId}")]
        public async Task<ActionResult<IEnumerable<DetalleVentaDTO>>> GetByVentaId(int ventaId)
        {
            try
            {
                var userRol = servicioUsuarios.ObtenerUsuarioRol();
                var sucursalId = "0";
                if (userRol is null)
                {
                    return BadRequest("El usuario no pertenece a ningun rol");
                }
                if (userRol != "admin")
                {
                    sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                }

                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El vendedor no pertenece a ninguna sucursal");
                }
                var detsVenta = await getAllDetVentaByVentaId.ExecuteAsync(ventaId, int.Parse(sucursalId));

                return Ok(detsVenta);
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
        
        [HttpGet("ventas/{idVenta}/detalles/{idDet}")]
        public async Task<ActionResult<DetalleVentaDTO>> Get(int idVenta, int idDet)
        {
            try
            {
                var detVenta = await getDetVentaById.ExecuteAsync(idVenta, idDet);

                return Ok(detVenta);
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

        [HttpGet("byInventarioId/{invId}")]
        public async Task<ActionResult<bool>> GetByInvId(int invId, [FromQuery] int ventaId, [FromQuery] int unidadMedidaId)
        {
            try
            {
                var det = await getDetVentaByInvId.ExecuteAsync(ventaId, invId, unidadMedidaId);
                if (det is null)
                {
                    return Ok(false);
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("byInventarioIdUpd/{invId}")]
        public async Task<ActionResult<bool>> GetByInvIdUpd(int invId, [FromQuery] int ventaId, [FromQuery] int unidadMedidaId, [FromQuery] int detId)
        {
            try
            {
                var det = await getDetVentaByInvIdUpd.ExecuteAsync(ventaId, invId, unidadMedidaId, detId);
                if (det is null)
                {
                    return Ok(false);
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(DetalleVentaCreacionDTO detalleVentaCreacionDTO)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El vendedor no pertenece a ninguna sucursal");
                }
                var resp = await postDetVenta.ExecuteAsync(detalleVentaCreacionDTO, int.Parse(sucursalId));
                if (resp)
                {
                    return Created();
                }
                return BadRequest("Ha ocurrido un error en la creacion del detalle de la venta");
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
        
        [HttpPut("ventas/{idVenta}/detalles/{idDet}")]
        public async Task<ActionResult> Put(DetalleVentaCreacionDTO detalleVentaCreacionDTO, int idVenta, int idDet)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El vendedor no pertenece a ninguna sucursal");
                }
                var resp = await putDetVenta.ExecuteAsync(detalleVentaCreacionDTO, idDet, idVenta, int.Parse(sucursalId));
                if (resp)
                {
                    return Ok();
                }
                return BadRequest("Ha ocurrido un error en la actualización del detalle de la venta");
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
        
        [HttpDelete("ventas/{ventaId}/detalles/{idDet}")]
        public async Task<ActionResult> Delete(int ventaId, int idDet)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El vendedor no pertenece a ninguna sucursal");
                }
                var resp = await deleteDetVenta.ExecuteAsync(idDet, ventaId, int.Parse(sucursalId));
                if (resp)
                {
                    return Ok();
                }
                return BadRequest("Ha ocurrido un error al eliminar el detalle de la venta");
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
