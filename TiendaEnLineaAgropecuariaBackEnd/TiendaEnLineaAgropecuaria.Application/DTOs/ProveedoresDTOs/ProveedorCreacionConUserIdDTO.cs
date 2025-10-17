using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs
{
    public class ProveedorCreacionConUserIdDTO: ProveedorCreacionDTO
    {
        public required string UserId { get; set; }
    }
}
