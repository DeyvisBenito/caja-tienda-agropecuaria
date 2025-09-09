using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Bodega
    {
        public int Id { get; set; }
        public int EstadoId { get; set; }
        public required string Nombre { get; set; }
        public required string Ubicacion { get; set; }
    }
}
