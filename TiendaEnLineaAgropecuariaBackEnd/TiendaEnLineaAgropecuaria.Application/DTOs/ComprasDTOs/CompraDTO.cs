using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs
{
    public class CompraDTO
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public string? EmailUser { get; set; }
        public ProveedorDTO? Proveedor { get; set; }
        public List<DetalleCompraDTO>? DetallesCompra { get; set; }
        public int SucursalId { get; set; }
        public required string Sucursal { get; set; }
        public int EstadoId { get; set; }
        public required string Estado { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
