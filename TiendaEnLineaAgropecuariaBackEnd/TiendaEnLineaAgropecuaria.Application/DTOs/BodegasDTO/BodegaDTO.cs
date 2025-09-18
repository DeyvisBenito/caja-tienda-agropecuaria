using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO
{
    public class BodegaDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Ubicacion { get; set; }
        public required string Estado { get; set; }
        public int EstadoId { get; set; }
    }
}
