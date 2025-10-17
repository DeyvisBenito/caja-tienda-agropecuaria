using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Usuario
    {
        public required string Email { get; set; }
        public string? Password { get; set; }
        public bool RecibirNotificaciones { get; set; }
        public int SucursalId { get; set; }
    }
}
