using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.VentasDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasQuerys;
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

        private readonly GetCompraPendiente getCompraPendiente;
        private readonly PostCompras postCompras;
        private readonly PutCompras putCompras;
        private readonly DeleteCompras deleteCompras;
        private readonly CancelCompra cancelCompra;
        private readonly ProcesarCompra procesarCompra;
        private readonly ServicioUsuarios servicioUsuarios;

        public VentasController(GetAllVentas getAllVentas, GetVentaById getVentaById, GetCompraPendiente getCompraPendiente,
                                    PostCompras postCompras, PutCompras putCompras, DeleteCompras deleteCompras, CancelCompra cancelCompra,
                                    ProcesarCompra procesarCompra, ServicioUsuarios servicioUsuarios)
        {
            this.getAllVentas = getAllVentas;
            this.getVentaById = getVentaById;
            this.getCompraPendiente = getCompraPendiente;
            this.postCompras = postCompras;
            this.putCompras = putCompras;
            this.deleteCompras = deleteCompras;
            this.cancelCompra = cancelCompra;
            this.procesarCompra = procesarCompra;
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

        /*
        [HttpPost]
        public async Task<ActionResult<int>> Post(CompraCreacionDTO compraCreacionDTO)
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

                var compraCreacion = new CompraCreacionUserSucursalId
                {
                    ProveedorNit = compraCreacionDTO.ProveedorNit,
                    SucursalId = intUsuarioSucursalId,
                    UserId = idUsuario,
                    Total = compraCreacionDTO.Total
                };

                var result = await postCompras.ExecuteAsync(compraCreacion);
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

        [HttpPost("{id}/procesar")]
        public async Task<ActionResult> Post(int id)
        {
            try
            {
                var idSucursal = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(idSucursal))
                {
                    return BadRequest("El usuario no pertenece a ninguna sucursal");
                }

                var result = await procesarCompra.ExecuteAsync(id, int.Parse(idSucursal));
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al procesar la compra");
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
        public async Task<ActionResult> Put(int id, CompraUpdateDTO compraCreacionDTO)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (sucursalId is null)
                {
                    return BadRequest("La sucursal del usuario vendedor no es valida");
                }
                var sucursalIdInt = int.Parse(sucursalId);
                var result = await putCompras.ExecuteAsync(id, compraCreacionDTO, sucursalIdInt);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al actualizar la compra");

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
                var result = await deleteCompras.ExecuteAsync(id);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al eliminar la compra");

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

        [HttpDelete("cancelCompra/{id}")]
        public async Task<ActionResult> Cancel(int id)
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (string.IsNullOrEmpty(sucursalId))
                {
                    return BadRequest("El usuario no pertenece a ninguna sucursal");
                }

                var result = await cancelCompra.ExecuteAsync(id, int.Parse(sucursalId));
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al cancelar la compra");

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
        } */
    }
}
