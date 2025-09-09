using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs;
using TiendaEnLineaAgropecuaria.Application.UseCases.AuthUseCases.AuthCommands;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly LoginConEmail loginConEmail;
        private readonly LoginConGoogle loginConGoogle;
        private readonly CrearToken crearToken;
        private readonly ValidarToken validarToken;
        private readonly GenerarPayloadDeGoogle generarPayloadDeGoogle;
        private readonly BuscarAppUsuario buscarAppUsuario;
        private readonly ResetearPassword resetearPassword;
        private readonly EnviarCorreos enviarCorreos;
        private readonly IConfiguration configuration;

        public AuthController(LoginConEmail loginConEmail, LoginConGoogle loginConGoogle,
                CrearToken crearToken, ValidarToken validarToken, GenerarPayloadDeGoogle generarPayloadDeGoogle,
                BuscarAppUsuario buscarAppUsuario, ResetearPassword resetearPassword, EnviarCorreos enviarCorreos, 
                IConfiguration configuration)
        {
            this.loginConEmail = loginConEmail;
            this.loginConGoogle = loginConGoogle;
            this.crearToken = crearToken;
            this.validarToken = validarToken;
            this.generarPayloadDeGoogle = generarPayloadDeGoogle;
            this.buscarAppUsuario = buscarAppUsuario;
            this.resetearPassword = resetearPassword;
            this.enviarCorreos = enviarCorreos;
            this.configuration = configuration;
        }

        // Validacion de Token
        [HttpGet("validarToken")]
        [AllowAnonymous]
        public ActionResult ValidarToken([FromQuery] string token)
        {
            bool respuesta = validarToken.ValidarTokenService(token);
            return Ok(respuesta);
        }



        // Metodo Login con Email
        [HttpPost("loginConEmail")]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAuthenticacionDTO>> LoginConEmail(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            try
            {
                var respuesta = await loginConEmail.Execute(credencialesUsuarioDTO);

                if (respuesta.EsExitoso)
                {
                    var token = await crearToken.ConstruirToken(credencialesUsuarioDTO, respuesta.IdUsuario!);
                    return Ok(token);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login incorrecto");
                    return ValidationProblem();
                }
            }
            catch (KeyNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        // Metodo Login con Google
        [HttpPost("loginConGoogle")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginConGoogle(CredencialesGoogleDTO credenciales)
        {
            try
            {
                var respuesta = await loginConGoogle.ExecuteAsync(credenciales.Credenciales!);
                if (respuesta.EsExitoso)
                {

                    var payload = await generarPayloadDeGoogle.DesifrarPayloadGoogle(credenciales);
                    var userCredenciales = new CredencialesUsuarioDTO
                    {
                        Email = payload.Email,
                    };

                    var token = await crearToken.ConstruirToken(userCredenciales, respuesta.IdUsuario!);

                    return Ok(token);
                }

                return BadRequest("Inicio de sesion invalido");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Solicitud de correo para recuperar password
        [HttpPost("recuperar-password")]
        [AllowAnonymous]
        public async Task<ActionResult> RecuperarPassword(RecuperarPasswordDTO recuperarPasswordDTO)
        {
            try
            {
                var credencialesUser = new CredencialesUsuarioDTO
                {
                    Email = recuperarPasswordDTO.Email
                };

                var tokenDeRecuperacion = await crearToken.ConstruirTokenDeRecuperacion(credencialesUser);
                await enviarCorreos.EnviarCorreo(recuperarPasswordDTO.Email, 
                                tokenDeRecuperacion.Asunto, tokenDeRecuperacion.Cuerpo);

                return Ok(new { success = true, message = "Se ha enviado un correo de restablecimiento de passwrord" });

            }catch(KeyNotFoundException e)
            {
                return Ok(new { success = false, message = e.Message });
            }
        }

        // Reseteo de Password
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetearPassword(ResetearPasswordDTO dto)
        {
            try
            {
                var usuarioDB = await buscarAppUsuario.BuscarPorEmail(dto.Email);

                await resetearPassword.ResetPassword(usuarioDB, dto.Token, dto.NuevoPassword);
                return Ok(new {success = true, message = "Password actualizado correctamente" });

            }catch(KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Validar token de reseteo de password
        [HttpPost("validar-token-resetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ValidarTokenResetPassword(CredencialesValidarTokenResetPasswordDTO dto)
        {
            try
            {
                var tokenEsValido = await validarToken.ValidarTokenResetPassword(dto);

                return Ok(tokenEsValido);
            }
            catch(KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
