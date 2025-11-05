using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string Nit { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public string? Email { get; set; }
        public required string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public IEnumerable<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
