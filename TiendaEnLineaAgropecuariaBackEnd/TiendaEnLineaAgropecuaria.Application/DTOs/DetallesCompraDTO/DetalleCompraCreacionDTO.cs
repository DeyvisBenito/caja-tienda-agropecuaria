using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO
{
    public class DetalleCompraCreacionDTO
    {
        [Required]
        public int CompraId { get; set; }
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
        public decimal PrecioCosto { get; set; }

    }
}
