using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class MovimientoTraslado
    {
        public int Id { get; set; }
        public int MovimientoId { get; set; }
        public Movimiento? Movimiento { get; set; }
        public int TrasladoId { get; set; }
        public Traslado? Traslado { get; set; }
    }
}
