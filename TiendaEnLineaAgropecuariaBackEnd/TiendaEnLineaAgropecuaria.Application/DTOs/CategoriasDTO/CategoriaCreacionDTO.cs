using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO
{
    public class CategoriaCreacionDTO
    {
        [Required]
        public int IdEstado { get; set; }
        [Required]
        public required string Nombre { get; set; }
    }
}
