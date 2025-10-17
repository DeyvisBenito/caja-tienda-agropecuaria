using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Perdida
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public decimal TotalPrecioCosto { get; set; }
        public decimal TotalPrecioVenta { get; set; }
        public int SucursalId { get; set; }
        public Sucursal? Sucursal { get; set; }
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
