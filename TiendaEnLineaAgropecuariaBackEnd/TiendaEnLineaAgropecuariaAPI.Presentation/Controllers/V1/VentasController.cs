using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.VentasDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.VentasUseCases.VentasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.VentasUseCases.VentasQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class VentasController : ControllerBase
    {
        private readonly GetAllVentas getAllVentas;
        private readonly GetVentaById getVentaById;
        private readonly PostVenta postVenta;
        private readonly PutVenta putVenta;
        private readonly DeleteVenta deleteVenta;
        private readonly CancelVenta cancelVenta;
        private readonly ProcesarVenta procesarVenta;
        private readonly ServicioUsuarios servicioUsuarios;

        public VentasController(GetAllVentas getAllVentas, GetVentaById getVentaById,
                                    PostVenta postVenta, PutVenta putVenta, DeleteVenta deleteVenta, CancelVenta cancelVenta,
                                    ProcesarVenta procesarVenta, ServicioUsuarios servicioUsuarios)
        {
            this.getAllVentas = getAllVentas;
            this.getVentaById = getVentaById;
            this.postVenta = postVenta;
            this.putVenta = putVenta;
            this.deleteVenta = deleteVenta;
            this.cancelVenta = cancelVenta;
            this.procesarVenta = procesarVenta;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<List<VentaDTO>>> Get()
        {
            try
            {
                var ventas = await getAllVentas.ExecuteAsync();
                foreach (var venta in ventas)
                {
                    var usuario = await servicioUsuarios.ObtenerUsuarioById(venta.UserId);
                    if (usuario is null)
                    {
                        return BadRequest($"El usuario que creó la venta numero {venta.Id} no existe, consulte a su administrador");
                    }
                    venta.EmailUser = usuario.Email;
                }

                return Ok(ventas);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDTO>> Get(int id)
        {
            try
            {
                var venta = await getVentaById.ExecuteAsync(id);
                var usuario = await servicioUsuarios.ObtenerUsuarioById(venta.UserId);
                if (usuario is null)
                {
                    return BadRequest("El usuario que creó la venta no existe, consulte a su administrador");
                }
                venta.EmailUser = usuario.Email;
                return Ok(venta);
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
        public async Task<ActionResult<int>> Post(VentaCreacion ventaCreacion)
        {
            try
            {
                var idUsuario = servicioUsuarios.ObtenerUsuarioId();
                if (idUsuario is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var usuarioSucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (usuarioSucursalId is null)
                {
                    return BadRequest("El usuario no pertenece a ninguna sucursal");
                }

                var intUsuarioSucursalId = int.Parse(usuarioSucursalId);

                var ventaCreacionWUserSucursalId = new VentaCreacionUserSucursalId
                {
                    ClienteNit = ventaCreacion.ClienteNit,
                    SucursalId = intUsuarioSucursalId,
                    UserId = idUsuario,
                    Total = ventaCreacion.Total
                };

                var result = await postVenta.ExecuteAsync(ventaCreacionWUserSucursalId);
                return Created(string.Empty, new { Id = result });
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
        
        [HttpPost("{id}/procesar/tipoPago/{tipoPagoId}")]
        public async Task<ActionResult<VueltoDTO>> Post(int id, int tipoPagoId, PagoDTO pago)
        {
            try
            {
                var idSucursal = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(idSucursal))
                {
                    return BadRequest("El usuario no pertenece a ninguna sucursal");
                }
                var userId = servicioUsuarios.ObtenerUsuarioId();
                if (userId is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var vuelto = await procesarVenta.ExecuteAsync(id, int.Parse(idSucursal), userId, tipoPagoId, pago);
                if (vuelto is not null) return Ok(vuelto);

                return BadRequest("Ha ocurrido un error al procesar la venta");
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, VentaUpdateDTO ventaUpdateDTO)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (sucursalId is null)
                {
                    return BadRequest("La sucursal del usuario vendedor no es valida");
                }
                var sucursalIdInt = int.Parse(sucursalId);
                var result = await putVenta.ExecuteAsync(id, ventaUpdateDTO, sucursalIdInt);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al actualizar la venta");

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
                var result = await deleteVenta.ExecuteAsync(id);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al eliminar la venta");

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

        [HttpDelete("cancelVenta/{id}")]
        public async Task<ActionResult> Cancel(int id)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El usuario no pertenece a ninguna sucursal");
                }

                var result = await cancelVenta.ExecuteAsync(id, int.Parse(sucursalId));
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al cancelar la venta");

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
