using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.DetallesVentaDTOs
{
    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int InventarioId { get; set; }
        public InventarioDTO? Inventario { get; set; }
        public int EstadoId { get; set; }
        public required string Estado { get; set; }
        public int UnidadMedidaId { get; set; }
        public required string UnidadMedida { get; set; }
        public int Cantidad { get; set; }
        public int? UnidadesPorCaja { get; set; }
        public decimal PrecioVenta { get; set; }
        public DateTime Fecha { get; set; }
    }
}
