using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Conversion
    {
        public int Id { get; set; }
        public int UnidadMedidaOrigenId { get; set; }
        public UnidadMedida? UnidadMedidaOrigen { get; set; }
        public int UnidadMedidaDestinoId { get; set; }
        public UnidadMedida? UnidadMedidaDestino { get; set; }

        // Permitir null en ListaPrecio con el descuento de conversiones de mayor a menor
        public int? ListaPrecioId { get; set; }
        public ListaPrecio? ListaPrecio { get; set; }
        public decimal Equivalencia { get; set; }

    }
}
