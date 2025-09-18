using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Carrito
    {
        public int Id { get; set; }
        public required string IdUser { get; set; }
        public DateTime Fecha { get; set; }
    }
}
