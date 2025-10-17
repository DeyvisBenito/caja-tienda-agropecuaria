using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ComprasController : ControllerBase
    {
        private readonly GetAllCompras getAllCompras;
        private readonly GetCompraById getCompraById;
        private readonly GetCompraPendiente getCompraPendiente;
        private readonly PostCompras postCompras;
        private readonly PutCompras putCompras;
        private readonly DeleteCompras deleteCompras;
        private readonly CancelCompra cancelCompra;
        private readonly ProcesarCompra procesarCompra;
        private readonly ServicioUsuarios servicioUsuarios;

        public ComprasController(GetAllCompras getAllCompras, GetCompraById getCompraById, GetCompraPendiente getCompraPendiente,
                                    PostCompras postCompras, PutCompras putCompras, DeleteCompras deleteCompras, CancelCompra cancelCompra,
                                    ProcesarCompra procesarCompra, ServicioUsuarios servicioUsuarios)
        {
            this.getAllCompras = getAllCompras;
            this.getCompraById = getCompraById;
            this.getCompraPendiente = getCompraPendiente;
            this.postCompras = postCompras;
            this.putCompras = putCompras;
            this.deleteCompras = deleteCompras;
            this.cancelCompra = cancelCompra;
            this.procesarCompra = procesarCompra;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompraDTO>>> Get()
        {
            try
            {
                var compras = await getAllCompras.ExecuteAsync();
                foreach (var compra in compras)
                {
                    var usuario = await servicioUsuarios.ObtenerUsuarioById(compra.UserId);
                    if (usuario is null)
                    {
                        return BadRequest($"El usuario que creó la compra numero {compra.Id} no existe, consulte a su administrador");
                    }
                    compra.EmailUser = usuario.Email;
                }

                return Ok(compras);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("pendiente")]
        public async Task<ActionResult<Compra>> GetCompraPendiente()
        {
            try
            {
                var sucursalId = servicioUsuarios.ObtenerUsuarioSucursalId();
                if (sucursalId is null)
                {
                    return BadRequest("El usuario no pertenece a ninguna sucursal, por lo tanto no puede hacer compras");
                }
                var sucursalIdInt = int.Parse(sucursalId);
                var compra = await getCompraPendiente.ExecuteAsync(sucursalIdInt);
                if (compra is null)
                {
                    return Ok(new { vacio = true });
                }
                return Ok(compra);
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

        [HttpGet("{id}")]
        public async Task<ActionResult<CompraDTO>> Get(int id)
        {
            try
            {
                var compra = await getCompraById.ExecuteAsync(id);
                var usuario = await servicioUsuarios.ObtenerUsuarioById(compra.UserId);
                if (usuario is null)
                {
                    return BadRequest("El usuario que creó la compra no existe, consulte a su administrador");
                }
                compra.EmailUser = usuario.Email;
                return Ok(compra);
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
        }
    }
}
