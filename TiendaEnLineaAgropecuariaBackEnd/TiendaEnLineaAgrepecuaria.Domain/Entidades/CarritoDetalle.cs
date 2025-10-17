using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class CarritoDetalle
    {
        public int Id { get; set; }
        public int CarritoId { get; set; }
        public int InventarioId { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime Fecha { get; set; }
        public Inventario? Inventario { get; set; }
    }
}
