using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO
{
    public class BodegaCreacionDTO
    {
        [Required]
        public int EstadoId { get; set; }
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Ubicacion { get; set; }
    }
}
