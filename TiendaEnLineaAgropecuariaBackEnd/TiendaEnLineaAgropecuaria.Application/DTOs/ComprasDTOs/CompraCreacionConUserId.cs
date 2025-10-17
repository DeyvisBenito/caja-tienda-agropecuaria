using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs
{
    public class CompraCreacionUserSucursalId
    {
        public required string UserId { get; set; }
        public required string ProveedorNit { get; set; }
        public int SucursalId { get; set; }
        public decimal Total { get; set; }
    }
}
