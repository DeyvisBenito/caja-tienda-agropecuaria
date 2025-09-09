using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public class ServicioUsuarios
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;

        public ServicioUsuarios(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
        }

        public string? ObtenerUsuarioId()
        {
            var claimUsuarioId = contextAccessor.HttpContext!.User.Claims.Where(x => x.Type == "usuarioId").FirstOrDefault();

            if (claimUsuarioId is null)
            {
                return null;
            }

            var usuarioId = claimUsuarioId.Value;

            return usuarioId;
        }

        public async Task<ApplicationUser?> ObtenerUsuario()
        {
            var emailClaim = contextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "email");

            if (emailClaim is null)
            {
                return null;
            }

            var usuario = await userManager.FindByEmailAsync(emailClaim!.Value);

            if (usuario is null)
            {
                return null;
            }

            return usuario;
        }
    }
}
