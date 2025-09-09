using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs
{
    public record ResetearPasswordDTO(string Email, string Token, string NuevoPassword);
    
}
