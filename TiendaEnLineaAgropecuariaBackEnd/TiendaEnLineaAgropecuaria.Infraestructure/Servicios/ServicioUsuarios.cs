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

        public string? ObtenerUsuarioRol()
        {
            var claimUserRol = contextAccessor.HttpContext!.User.Claims.Where(x => x.Type == "rol").FirstOrDefault();

            if (claimUserRol is null)
            {
                return null;
            }

            var usuarioId = claimUserRol.Value;

            return usuarioId;
        }

        public string? ObtenerUsuarioSucursalId()
        {
            var claimSucursalId = contextAccessor.HttpContext!.User.Claims.Where(x => x.Type == "sucursalId").FirstOrDefault();

            if (claimSucursalId is null)
            {
                return null;
            }

            var sucursalId = claimSucursalId.Value;

            return sucursalId;
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

        public async Task<ApplicationUser?> ObtenerUsuarioById(string id)
        {
           

            var usuario = await userManager.FindByIdAsync(id);

            if (usuario is null)
            {
                return null;
            }

            return usuario;
        }
    }
}
