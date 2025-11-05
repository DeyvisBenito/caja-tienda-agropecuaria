using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.DetallesVentaDTOs
{
    public class DetalleVentaCreacionDTO
    {
        [Required]
        public int VentaId { get; set; }
        [Required]
        public int InventarioId { get; set; }
        [Required]
        public int EstadoId { get; set; }
        [Required]
        public int UnidadMedidaId { get; set; }
        public int? UnidadesPorCaja { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal PrecioVenta { get; set; }
        [Required]
        public decimal Descuento { get; set; }
        [Required]
        public decimal PrecioVentaConDescuento { get; set; }
        [Required]
        public decimal Total { get; set; }
    }
}
