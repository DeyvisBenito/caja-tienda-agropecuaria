using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class DetalleMovimiento
    {
        public int Id { get; set; }
        public int MovimientoId { get; set; }
        public Movimiento? Movimiento { get; set; }
        public int InventarioId { get; set; }
        public Inventario? Inventario { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int UnidadMedidaId { get; set; }
        public UnidadMedida? UnidadMedida { get; set; }
    }
}
