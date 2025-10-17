using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Compra
    {
        public int Id { get; set; }
        public required string IdUser { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }
        public List<DetalleCompra>? DetallesCompra { get; set; }
        public int SucursalId { get; set; }
        public Sucursal? Sucursal { get; set; }
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
