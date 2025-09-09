using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public class ResetearPassword
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ResetearPassword(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // Resetea el password del usuario
        public async Task ResetPassword(ApplicationUser usuario, string token, string NuevaPassword)
        {
            var result = await userManager.ResetPasswordAsync(usuario, token, NuevaPassword);

            if (!result.Succeeded)
            {
                var primerError = result.Errors.FirstOrDefault()?.Description ?? "Error desconocido";
                throw new Exception(primerError);
            }

        }
    }
}
