using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class ListaPrecio
    {
        public int Id { get; set; }
        public required string Tipo { get; set; }
        public decimal DescuentoPorcentaje { get; set; }
    }
}
