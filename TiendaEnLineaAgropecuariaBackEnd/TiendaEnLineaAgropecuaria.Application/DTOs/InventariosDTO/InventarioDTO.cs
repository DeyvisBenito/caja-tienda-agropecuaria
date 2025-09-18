using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO
{
    public class InventarioDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int TipoProductoId { get; set; }
        public int EstadoId { get; set; }
        public int BodegaId { get; set; }
        public required string Marca { get; set; }
        public decimal Precio { get; set; }
        public required string UrlFoto { get; set; }
        public string? Descripcion { get; set; }
        public int Stock { get; set; }
        public required string TipoProducto { get; set; }
        public required string Estado { get; set; }
        public required string Bodega { get; set; }
    }
}
