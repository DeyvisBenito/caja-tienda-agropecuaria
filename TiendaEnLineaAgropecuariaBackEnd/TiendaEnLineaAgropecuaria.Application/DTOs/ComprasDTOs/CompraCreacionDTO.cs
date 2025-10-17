using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs
{
    public class CompraCreacionDTO
    {
        public required string ProveedorNit { get; set; }
        public decimal Total { get; set; }
    }
}
