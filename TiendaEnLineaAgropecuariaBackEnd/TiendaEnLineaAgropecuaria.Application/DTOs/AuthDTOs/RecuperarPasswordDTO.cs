using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs
{
    public class RecuperarPasswordDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string Email { get; set; }
    }
}
