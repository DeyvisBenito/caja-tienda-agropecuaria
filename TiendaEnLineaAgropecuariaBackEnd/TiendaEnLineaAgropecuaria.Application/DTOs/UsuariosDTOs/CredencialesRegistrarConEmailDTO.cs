using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.UsuariosDTOs
{
    public class CredencialesRegistrarConEmailDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener una longitud máxima de {1} caracteres")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool RecibirNotificaciones { get; set; }
    }
}
