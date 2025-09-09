using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
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
    public class ValidarToken
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public ValidarToken(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public bool ValidarTokenService(string token)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            var tokenHandler = new JwtSecurityTokenHandler();
            var validatorParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]!)),
                ClockSkew = TimeSpan.Zero
            };


            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, validatorParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ValidarTokenResetPassword(CredencialesValidarTokenResetPasswordDTO dto)
        {
            var usuarioDB = await userManager.FindByEmailAsync(dto.Email);

            if(usuarioDB is null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            var tokenEsValido = await userManager.VerifyUserTokenAsync(usuarioDB,
                        userManager.Options.Tokens.PasswordResetTokenProvider,
                        "ResetPassword", dto.TokenResetPassword);


            if (tokenEsValido)
            {
                return true;
            }
            return false;
        }
    }
}
