using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Datos
{
    public class ApplicationUser : IdentityUser
    {
        public bool RecibirNotificaciones { get; set; }
        public int? SucursalId { get; set; }
        public Sucursal? Sucursal { get; set; }
        public int? EstadoId { get; set; }
        public Estado? Estado { get; set; }
        public bool PerfilCompletado { get; set; } 
    }
}
