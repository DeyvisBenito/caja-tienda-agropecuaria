using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class DetalleCompra
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        public Compra? Compra { get; set; }
        public int InventarioId { get; set; }
        public Inventario? Inventario { get; set; }
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }
        public int UnidadMedidaId { get; set; }
        public UnidadMedida? UnidadMedida { get; set; }
        public int Cantidad { get; set; }
        public int? UnidadesPorCaja { get; set; }
        public decimal PrecioCosto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
