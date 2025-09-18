using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO
{
    public class BodegaCreacionConUserIdDTO : BodegaCreacionDTO
    {
        public required string UserId { get; set; }
    }
}
