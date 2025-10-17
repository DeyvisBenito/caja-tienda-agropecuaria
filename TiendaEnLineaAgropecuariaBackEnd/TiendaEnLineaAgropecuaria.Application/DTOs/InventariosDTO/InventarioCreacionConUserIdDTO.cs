using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO
{
    public class InventarioCreacionConUserIdDTO: InventarioCreacionDTO
    {
        public required string UrlFoto { get; set; }
        public required string IdUser { get; set; }
        public int SucursalId { get; set; }
    }
}
