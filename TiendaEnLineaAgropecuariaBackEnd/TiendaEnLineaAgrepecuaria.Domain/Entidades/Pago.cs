using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Pago
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int TipoPagoId { get; set; }
        public TipoPago? TipoPago { get; set; }
        public int VentaId { get; set; }
        public Venta? Venta { get; set; }
        public int SucursalId { get; set; }
        public Sucursal? Sucursal { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal TotalPago { get; set; }
        public decimal Vuelto { get; set; }
    }
}
