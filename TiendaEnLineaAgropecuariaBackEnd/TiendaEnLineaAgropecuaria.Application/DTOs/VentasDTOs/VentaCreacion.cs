using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.VentasDTOs
{
    public class VentaCreacion
    {
        [Required]
        public required string ClienteNit { get; set; }
        [Required]
        public decimal Total { get; set; }
    }
}
