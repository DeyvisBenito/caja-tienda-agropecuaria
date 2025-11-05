using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.VentasDTOs
{
    public class VentaCreacionUserSucursalId
    {
        public required string UserId { get; set; }
        public required string ClienteNit { get; set; }
        public int SucursalId { get; set; }
        public decimal Total { get; set; }
    }
}
