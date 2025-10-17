using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO
{
    public class SucursalCreacionConUserIdDTO : SucursalCreacionDTO
    {
        public required string UserId { get; set; }
    }
}
