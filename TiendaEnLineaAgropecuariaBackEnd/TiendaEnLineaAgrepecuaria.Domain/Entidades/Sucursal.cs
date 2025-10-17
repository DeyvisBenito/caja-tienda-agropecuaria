using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Sucursal
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }
        public required string Nombre { get; set; }
        public required string Ubicacion { get; set; }
        
    }
}
