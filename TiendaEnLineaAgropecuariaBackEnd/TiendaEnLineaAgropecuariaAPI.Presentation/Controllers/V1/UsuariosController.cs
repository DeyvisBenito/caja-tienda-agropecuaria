using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.UsuariosDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.UsuariosUseCases.UsuariosCommands;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioUsuarios;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "AdminPolicy")]
    public class UsuariosController : ControllerBase
    {
        private readonly RegistrarUsuarioConEmail registrarUsuarioConEmail;
        private readonly CrearToken crearToken;
        private readonly RepositorioUsuarios repositorioUsuarios;

        public UsuariosController(RegistrarUsuarioConEmail registrarUsuarioConEmail,
                CrearToken crearToken, RepositorioUsuarios repositorioUsuarios)
        {
            this.registrarUsuarioConEmail = registrarUsuarioConEmail;
            this.crearToken = crearToken;
            this.repositorioUsuarios = repositorioUsuarios;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> Get()
        {
            try
            {
                var usuarios = await repositorioUsuarios.Get();

                return Ok(usuarios);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> Get(string id)
        {
            try
            {
                var usuario = await repositorioUsuarios.GetById(id);

                return Ok(usuario);
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

        [HttpGet("byemailbool/{email}")]
        public async Task<ActionResult> GetByEmailBool(string email)
        {
            try
            {
                var resp = await repositorioUsuarios.GetByEmailBool(email);

                return Ok(resp);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpPost("registroConEmail")]
        public async Task<ActionResult<RespuestaAuthenticacionDTO>> RegistrarConEmail(
                        CredencialesRegistrarConEmailDTO credencialesRegistrarConEmailDTO)
        {
            var resultadoRegistro = await registrarUsuarioConEmail.Execute(credencialesRegistrarConEmailDTO);

            if (resultadoRegistro.EsExitoso)
            {
                try
                {
                    var credencialesUsuarioDTO = new CredencialesUsuarioDTO
                    {
                        Email = credencialesRegistrarConEmailDTO.Email
                    };
                    var sucursalIdString = resultadoRegistro.SucursalId.ToString();
                    var token = await crearToken.ConstruirToken(credencialesUsuarioDTO, resultadoRegistro.IdUsuario!, sucursalIdString!);

                    return Ok(token);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }

            }
            else
            {
                foreach (var error in resultadoRegistro.Errores)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return ValidationProblem();
            }


        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, UsuarioUpdateDTO usuarioUpdateDTO)
        {
            try
            {
                var resp = await repositorioUsuarios.UpdateUsuario(id, usuarioUpdateDTO);
                if (resp)
                {
                    return Ok();
                }
                return BadRequest("Ha ocurrido un error al actualizar el usuario");
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
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var resp = await repositorioUsuarios.DeleteUser(id);
                if (resp)
                {
                    return Ok();
                }
                return BadRequest("Ha ocurrido un error al eliminar el usuario");
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
