using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.ClientesDTOs
{
    public class ClienteCreacionConUserId : ClienteCreacionDTO
    {
        public required string UserId { get; set; }
    }
}
