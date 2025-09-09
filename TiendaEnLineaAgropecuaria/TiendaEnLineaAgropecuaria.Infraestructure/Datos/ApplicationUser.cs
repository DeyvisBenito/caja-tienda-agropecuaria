using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Datos
{
    public class ApplicationUser : IdentityUser
    {
        public bool RecibirNotificaciones { get; set; }
        public bool PerfilCompletado { get; set; } 
    }
}
