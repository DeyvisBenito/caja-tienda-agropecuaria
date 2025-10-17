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
        public string? IdUser { get; set; }
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public int TipoMedidaId { get; set; }
        public TipoMedida? TipoMedida { get; set; }
        public required string Nombre { get; set; }
        
        
    }
}
