using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.ClientesDTOs
{
    public class ClienteCreacionDTO
    {
        [Required]
        public required string Nit { get; set; }
        [Required]
        public required string Nombres { get; set; }
        [Required]
        public required string Apellidos { get; set; }
        public string? Email { get; set; }
        [Required]
        public required string Telefono { get; set; }
    }
}
