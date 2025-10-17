using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Movimiento
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int TipoMovimientoId { get; set; }
        public TipoMovimiento? TipoMovimiento { get; set; }
        public required string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
