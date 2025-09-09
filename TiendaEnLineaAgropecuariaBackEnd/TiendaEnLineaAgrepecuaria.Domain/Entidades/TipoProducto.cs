using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class TipoProducto
    {
        public int Id { get; set; }
        public required string IdUser { get; set; }
        public int EstadoId { get; set; }
        public int CategoriaId { get; set; }
        public required string Nombre { get; set; }
        public Estado? Estado { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
