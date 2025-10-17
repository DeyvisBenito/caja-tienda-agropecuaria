using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.UsuariosDTOs
{
    public class UsuarioUpdateDTO
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public int SucursalId { get; set; }
        [Required]
        public int EstadoId { get; set; }
    }
}
