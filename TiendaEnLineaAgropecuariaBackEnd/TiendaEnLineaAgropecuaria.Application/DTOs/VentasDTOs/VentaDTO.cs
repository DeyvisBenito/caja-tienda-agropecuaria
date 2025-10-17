using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgropecuaria.Application.DTOs.ClientesDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesVentaDTOs;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.VentasDTOs
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public string? EmailUser { get; set; }
        public int ClienteId { get; set; }
        public ClienteDTO? Cliente { get; set; }
        public List<DetalleVentaDTO>? DetallesVenta { get; set; }
        public int SucursalId { get; set; }
        public required string Sucursal { get; set; }
        public int EstadoId { get; set; }
        public required string Estado { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
