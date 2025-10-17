using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class UnidadMedida
    {
        public int Id { get; set; }
        public required string Medida { get; set; }
        public required string Abreviatura { get; set; }
        public int TipoMedidaId { get; set; }
        public TipoMedida? TipoMedida { get; set; }
    }
}
