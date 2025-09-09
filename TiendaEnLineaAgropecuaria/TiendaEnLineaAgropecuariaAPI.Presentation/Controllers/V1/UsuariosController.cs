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
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsuariosController: ControllerBase
    {
        private readonly RegistrarUsuarioConEmail registrarUsuarioConEmail;
        private readonly CrearToken crearToken;

        public UsuariosController(RegistrarUsuarioConEmail registrarUsuarioConEmail,
                CrearToken crearToken)
        {
            this.registrarUsuarioConEmail = registrarUsuarioConEmail;
            this.crearToken = crearToken;
        }

        [HttpPost("registroConEmail")]
        [AllowAnonymous]
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

                    var token = await crearToken.ConstruirToken(credencialesUsuarioDTO);

                    return Ok(token);
                }
                catch(KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                
            }
            else
            {
                foreach(var error in resultadoRegistro.Errores)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return ValidationProblem();
            }


        }
        
    }
}
