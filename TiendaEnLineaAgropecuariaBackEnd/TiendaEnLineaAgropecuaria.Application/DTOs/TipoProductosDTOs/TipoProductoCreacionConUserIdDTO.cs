using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.TipoProductosDTOs
{
    public class TipoProductoCreacionConUserIdDTO : TipoProductoCreacionDTO
    {
        public required string IdUser { get; set; }
    }
}
