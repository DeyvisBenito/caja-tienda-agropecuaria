using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public required string IdUser { get; set; }
        public int IdEstaado { get; set; }
        public int IdCategoria { get; set; }

        //Faltan propiedades, solo estoy haciendo pruebas
    }
}
