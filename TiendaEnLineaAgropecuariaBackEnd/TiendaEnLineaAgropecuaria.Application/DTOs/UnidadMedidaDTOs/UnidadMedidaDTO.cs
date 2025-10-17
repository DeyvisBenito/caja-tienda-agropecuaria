using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.UnidadMedidaDTOs
{
    public class UnidadMedidaDTO
    {
        public int Id { get; set; }
        public required string Medida { get; set; }
        public required string Abreviatura { get; set; }
        public int TipoMedidaId { get; set; }
        public required string TipoMedida { get; set; }
    }
}
