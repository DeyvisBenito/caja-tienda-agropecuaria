using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Inventario
    {
        public int Id { get; set; }
        public required string Codigo { get; set; }
        public string? IdUser { get; set; }
        public required string Nombre { get; set; }
        public int TipoProductoId { get; set; }
        public TipoProducto? TipoProducto { get; set; }
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }
        public int SucursalId { get; set; }
        public Sucursal? Sucursal { get; set; }
        public required string Marca { get; set; }
        public decimal PrecioCostoPromedio { get; set; }
        public decimal PrecioVenta { get; set; }
        public required string UrlFoto { get; set; }
        public string? Descripcion { get; set; }
        public int Stock { get; set; }
        public int UnidadMedidaId { get; set; }
        public UnidadMedida? UnidadMedida { get; set; }



    }
}
