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
        public required string Codigo { get; set; }
        public required string Nombre { get; set; }
        public int TipoProductoId { get; set; }
        public required string TipoProducto { get; set; }
        public int EstadoId { get; set; }
        public required string Estado { get; set; }
        public int SucursalId { get; set; }
        public required string Sucursal { get; set; }
        public required string Marca { get; set; }
        public decimal PrecioCostoPromedio { get; set; }
        public decimal PrecioVenta { get; set; }
        public required string UrlFoto { get; set; }
        public string? Descripcion { get; set; }
        public int Stock { get; set; }
        public int UnidadMedidaId { get; set; }
        public required string UnidadMedida { get; set; }

    }
}
