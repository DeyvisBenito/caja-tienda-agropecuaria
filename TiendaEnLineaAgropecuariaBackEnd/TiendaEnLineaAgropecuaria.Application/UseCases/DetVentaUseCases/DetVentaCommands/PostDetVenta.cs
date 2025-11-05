using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesVentaDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.DetVentaUseCases.DetVentaCommands
{
    public class PostDetVenta
    {
        private readonly IRepositorioDetVenta repositorioDetVenta;

        public PostDetVenta(IRepositorioDetVenta repositorioDetVenta)
        {
            this.repositorioDetVenta = repositorioDetVenta;
        }

        public async Task<bool> ExecuteAsync(DetalleVentaCreacionDTO detalleVentaCreacionDTO, int sucursalId)
        {
            var detalle = new DetalleVenta
            {
                Cantidad = detalleVentaCreacionDTO.Cantidad,             
                VentaId = detalleVentaCreacionDTO.VentaId,
                EstadoId = detalleVentaCreacionDTO.EstadoId,
                Fecha = DateTime.UtcNow,
                InventarioId = detalleVentaCreacionDTO.InventarioId,
                PrecioVentaUnidadMinima = detalleVentaCreacionDTO.PrecioVenta,
                UnidadMedidaId = detalleVentaCreacionDTO.UnidadMedidaId,
                UnidadesPorCaja = detalleVentaCreacionDTO.UnidadesPorCaja,
                Descuento = detalleVentaCreacionDTO.Descuento,
                PrecioVentaConDescuentoUnidadMinima = detalleVentaCreacionDTO.PrecioVentaConDescuento,
                Total = detalleVentaCreacionDTO.Total,
                
            };
            var resp = await repositorioDetVenta.NewDetalle(detalle, sucursalId);

            return resp;
        }
    }
}
