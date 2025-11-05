using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.DTOs.ClientesDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.ClientesUseCases.ClientesCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ClientesUseCases.ClientesQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly GetAllClientes getAllClientes;
        private readonly GetClienteById getClienteById;
        private readonly GetClienteByVentaId getClienteByVentaId;
        private readonly PostCliente postCliente;
        private readonly PutCliente putCliente;
        private readonly DeleteCliente deleteCliente;
        private readonly ServicioUsuarios servicioUsuarios;

        public ClientesController(GetAllClientes getAllClientes, GetClienteById getClienteById, GetClienteByVentaId getClienteByVentaId, PostCliente postCliente,
                                    PutCliente putCliente, DeleteCliente deleteCliente, ServicioUsuarios servicioUsuarios)
        {
            this.getAllClientes = getAllClientes;
            this.getClienteById = getClienteById;
            this.getClienteByVentaId = getClienteByVentaId;
            this.postCliente = postCliente;
            this.putCliente = putCliente;
            this.deleteCliente = deleteCliente;
            this.servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get()
        {
            try
            {
                var clientes = await getAllClientes.ExecuteAsync();

                return Ok(clientes);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            try
            {
                var cliente = await getClienteById.ExecuteAsync(id);

                return Ok(cliente);
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

        [HttpGet("venta/{idVenta}")]
        public async Task<ActionResult<ClienteDTO>> GetByVentaId(int idVenta)
        {
            try
            {
                var cliente = await getClienteByVentaId.ExecuteAsync(idVenta);

                return Ok(cliente);
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
        public async Task<ActionResult> Post(ClienteCreacionDTO clienteCreacionDTO)
        {
            try
            {
                var idUsuario = servicioUsuarios.ObtenerUsuarioId();
                if (idUsuario is null)
                {
                    return BadRequest("El usuario no esta logueado");
                }

                var clienteConUserId = new ClienteCreacionConUserId
                {
                    Nit = clienteCreacionDTO.Nit,
                    Apellidos = clienteCreacionDTO.Apellidos,
                    Nombres = clienteCreacionDTO.Nombres,
                    Telefono = clienteCreacionDTO.Telefono,
                    UserId = idUsuario,
                    Email = clienteCreacionDTO.Email
                };

                var result = await postCliente.ExecuteAsync(clienteConUserId);
                if (result) return Created();

                return BadRequest("Ha ocurrido un error al agregar el Cliente");
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
        public async Task<ActionResult> Put(int id, ClienteCreacionDTO clienteCreacionDTO)
        {
            try
            {
                var result = await putCliente.ExecuteAsync(id, clienteCreacionDTO);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al actualizar el Cliente");

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await deleteCliente.ExecuteAsync(id);
                if (result) return Ok();

                return BadRequest("Ha ocurrido un error al eliminar el Cliente");

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
    }
}
