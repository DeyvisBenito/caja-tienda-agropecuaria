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
        public int IdUser { get; set; }
        public int TipoProductoId { get; set; }
        public int EstadoId { get; set; }
        public int BodegaId { get; set; }
        public required string Marca { get; set; }
        public decimal Precio { get; set; }
        public required string UrlFoto { get; set; }
        public string? Descripcion { get; set; }
        public int Stock { get; set; }
        public TipoProducto? TipoProducto { get; set; }
        public Estado? Estado { get; set; }
        public Bodega? Bodega { get; set; }

    }
}
