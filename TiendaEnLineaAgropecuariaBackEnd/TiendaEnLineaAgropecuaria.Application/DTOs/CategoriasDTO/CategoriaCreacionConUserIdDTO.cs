using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO
{
    public class CategoriaCreacionConUserIdDTO : CategoriaCreacionDTO
    {
        public required string UserId { get; set; }
    }
}
