using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Proveedor
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string Nit { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required string Telefono { get; set; }
        public required string Ubicacion { get; set; }
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }
    }
}
