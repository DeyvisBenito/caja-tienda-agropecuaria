using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public class BuscarAppUsuario
    {
        private readonly UserManager<ApplicationUser> userManager;

        public BuscarAppUsuario(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // Buscar usuario por Email
        public async Task<ApplicationUser> BuscarPorEmail(string email)
        {
            var usuarioDB = await userManager.FindByEmailAsync(email);
            if(usuarioDB is null)
            {
                throw new KeyNotFoundException("usuario no encontrado");
            }

            return usuarioDB;
        }
    }
}
