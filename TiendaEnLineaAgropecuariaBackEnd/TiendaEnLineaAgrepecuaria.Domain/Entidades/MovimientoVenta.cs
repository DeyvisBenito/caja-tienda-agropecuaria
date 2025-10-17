using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class MovimientoVenta
    {
        public int Id { get; set; }
        public int MovimientoId { get; set; }
        public Movimiento? Movimiento { get; set; }
        public int VentaId { get; set; }
        public Venta? Venta { get; set; }
    }
}
