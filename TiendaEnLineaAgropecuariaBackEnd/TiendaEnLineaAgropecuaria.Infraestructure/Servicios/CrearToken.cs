using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public class CrearToken
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public CrearToken(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        // Construir token de logueo JWT
        public async Task<RespuestaAuthenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO, string usuarioId)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", credencialesUsuarioDTO.Email),
                new Claim("usuarioId", usuarioId)
            };

            var usuarioDB = await userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);

            if (usuarioDB is null)
            {
                throw new KeyNotFoundException("Login incorrecto");
            }

            var claimsDB = await userManager.GetClaimsAsync(usuarioDB!);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]!));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMonths(5);

            var tokenDeSeguridad = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiracion,
                signingCredentials: credenciales
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeSeguridad);

            return new RespuestaAuthenticacionDTO
            {
                Token = token,
                FechaExpiracion = expiracion,
                EsExitoso = true
            };
        }

        // Construir token de recuperacion de password
        public async Task<RespuestaConstruirTokenRecuperacionDTO> ConstruirTokenDeRecuperacion(
                    CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var usuarioDB = await userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
            if(usuarioDB is null)
            {
                throw new KeyNotFoundException("Se ha enviado un correo de restablecimiento de passwrord");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(usuarioDB!);

            var urlBase = configuration["FrontEnd:urlBase"];

            var linkResetPassword =
                    $"{urlBase}/auth/resetearPassword?email={Uri.EscapeDataString(credencialesUsuarioDTO.Email)}&token={Uri.EscapeDataString(token)}";

            var cuerpo = $@"
                <p>Solicitaste restablecer tu contraseña.</p>
                <p><a href=""{linkResetPassword}"">Haz clic aquí para restablecerla</a></p>
                <p>Si no fuiste tú, ignora este correo.</p>";

            var respuesta = new RespuestaConstruirTokenRecuperacionDTO
            {
                Asunto = "Restablecer contraseña",
                Cuerpo = cuerpo,
                EmailDestino = credencialesUsuarioDTO.Email
            };

            return respuesta;
        }
    }
}
