using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public class GenerarPayloadDeGoogle
    {
        private readonly IConfiguration configuration;
        public GenerarPayloadDeGoogle(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        // Desifra el Payload de Google y lo sirve
        public async Task<GoogleJsonWebSignature.Payload> DesifrarPayloadGoogle(CredencialesGoogleDTO credenciales)
        {

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>
                        {
                           configuration["GoogleSettings:ClientId"]!
                        }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(credenciales.Credenciales, settings);

            return payload;
        }
        
    }
}
