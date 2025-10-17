using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetCompraUseCases.DetCompraCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetCompraUseCases.DetCompraQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class DetalleCompraController : ControllerBase
    {
        private readonly GetAllDetCompra getAllDetCompra;
        private readonly GetAllDetCompraByCompraId getAllDetCompraByCompraId;
        private readonly GetDetCompraById getDetCompraById;

        private readonly PostDetCompra postDetCompra;
        private readonly GetDetByInvId getDetByInvId;
        private readonly PutDetCompra putDetCompra;
        private readonly DeleteDetCompra deleteDetCompra;
        private readonly ServicioUsuarios servicioUsuarios;

        public DetalleCompraController(GetAllDetCompra getAllDetCompra, GetAllDetCompraByCompraId getAllDetCompraByCompraId,
                                        GetDetCompraById getDetCompraById, PutDetCompra putDetCompra, DeleteDetCompra deleteDetCompra,
                                        PostDetCompra postDetCompra, GetDetByInvId getDetByInvId, ServicioUsuarios servicioUsuarios)
        {
            this.getAllDetCompra = getAllDetCompra;
            this.getAllDetCompraByCompraId = getAllDetCompraByCompraId;
            this.getDetCompraById = getDetCompraById;
            this.postDetCompra = postDetCompra;
            this.getDetByInvId = getDetByInvId;
            this.putDetCompra = putDetCompra;
            this.deleteDetCompra = deleteDetCompra;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompraDTO>>> Get()
        {
            try
            {
                var detsCompra = await getAllDetCompra.ExecuteAsync();

                return Ok(detsCompra);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("bycompra/{compraId}")]
        public async Task<ActionResult<IEnumerable<DetalleCompraDTO>>> GetByCompraId(int compraId)
        {
            try
            {
                var userRol = servicioUsuarios.ObtenerUsuarioRol();
                var sucursalId = "0";
                if (userRol is null)
                {
                    return BadRequest("El usuario no pertenece a ningun rol");
                }
                if(userRol != "admin")
                {
                    sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                }
                
                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El vendedor no pertenece a ninguna sucursal");
                }
                var detsCompra = await getAllDetCompraByCompraId.ExecuteAsync(compraId, int.Parse(sucursalId));

                return Ok(detsCompra);
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

        [HttpGet("compras/{idCompra}/detalles/{idDet}")]
        public async Task<ActionResult<DetalleCompraDTO>> Get(int idCompra, int idDet)
        {
            try
            {
                var detCompra = await getDetCompraById.ExecuteAsync(idCompra, idDet);

                return Ok(detCompra);
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
        public async Task<ActionResult<bool>> GetByInvId(int invId, [FromQuery] int compraId)
        {
            try
            {
                var det = await getDetByInvId.ExecuteAsync(compraId, invId);
                if(det is null)
                {
                    return Ok(false);
                }
                return Ok(true);
            }catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(DetalleCompraCreacionDTO detalleCompraCreacionDTO)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El vendedor no pertenece a ninguna sucursal");
                }
                var resp = await postDetCompra.ExecuteAsync(detalleCompraCreacionDTO, int.Parse(sucursalId));
                if (resp)
                {
                    return Created();
                }
                return BadRequest("Ha ocurrido un error en la creacion del detalle de la compra");
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

        [HttpPut("compras/{idCompra}/detalles/{idDet}")]
        public async Task<ActionResult> Put(DetalleCompraCreacionDTO detalleCompraCreacionDTO, int idCompra, int idDet)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El vendedor no pertenece a ninguna sucursal");
                }
                var resp = await putDetCompra.ExecuteAsync(detalleCompraCreacionDTO, idDet, idCompra, int.Parse(sucursalId));
                if (resp)
                {
                    return Ok();
                }
                return BadRequest("Ha ocurrido un error en la creacion del detalle de la compra");
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

        [HttpDelete("compras/{idCompra}/detalles/{idDet}")]
        public async Task<ActionResult<DetalleCompraDTO>> Delete(int idCompra, int idDet)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El vendedor no pertenece a ninguna sucursal");
                }
                var resp = await deleteDetCompra.ExecuteAsync(idDet, idCompra, int.Parse(sucursalId));
                if (resp)
                {
                    return Ok();
                }
                return BadRequest("Ha ocurrido un error al eliminar el detalle de la compra");
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
