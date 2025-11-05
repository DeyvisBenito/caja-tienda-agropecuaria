using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesVentaDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.DetVentaUseCases.DetVentaQuerys
{
    public class GetDetVentaById
    {
        private readonly IRepositorioDetVenta repositorioDetVenta;

        public GetDetVentaById(IRepositorioDetVenta repositorioDetVenta)
        {
            this.repositorioDetVenta = repositorioDetVenta;
        }

        public async Task<DetalleVentaDTO> ExecuteAsync(int idVenta, int idDet)
        {
            var detalleVenta = await repositorioDetVenta.GetById(idVenta, idDet);

            var detalleVentaDTO = new DetalleVentaDTO
            {
                Id = detalleVenta.Id,
                Cantidad = detalleVenta.Cantidad,
                VentaId = detalleVenta.VentaId,
                Estado = detalleVenta.Estado!.Nombre,
                InventarioId = detalleVenta.InventarioId,
                UnidadMedidaId = detalleVenta.UnidadMedidaId,
                UnidadMedida = detalleVenta.UnidadMedida!.Medida,
                EstadoId = detalleVenta.EstadoId,
                Fecha = detalleVenta.Fecha,
                PrecioVenta = detalleVenta.PrecioVentaUnidadMinima,
                UnidadesPorCaja = detalleVenta.UnidadesPorCaja,
                Descuento = detalleVenta.Descuento,
                PrecioVentaConDescuento = detalleVenta.PrecioVentaConDescuentoUnidadMinima,
                Total = detalleVenta.Total,

                Inventario = new InventarioDTO
                {
                    Id = detalleVenta.Inventario!.Id,
                    Codigo = detalleVenta.Inventario.Codigo,
                    Descripcion = detalleVenta.Inventario.Descripcion,
                    EstadoId = detalleVenta.Inventario.EstadoId,
                    Estado = detalleVenta.Inventario.Estado!.Nombre,
                    Marca = detalleVenta.Inventario.Marca,
                    Nombre = detalleVenta.Inventario.Nombre,
                    Sucursal = detalleVenta.Inventario.Sucursal!.Nombre,
                    SucursalId = detalleVenta.Inventario.SucursalId,
                    TipoProductoId = detalleVenta.Inventario.TipoProductoId,
                    TipoProducto = detalleVenta.Inventario.TipoProducto!.Nombre,
                    UnidadMedida = detalleVenta.Inventario.UnidadMedida!.Medida,
                    UnidadMedidaId = detalleVenta.Inventario.UnidadMedidaId,
                    PrecioCostoPromedio = detalleVenta.Inventario.PrecioCostoPromedio,
                    PrecioVenta = detalleVenta.Inventario.PrecioVenta,
                    UrlFoto = detalleVenta.Inventario.UrlFoto,
                    Stock = detalleVenta.Inventario.Stock
                }
            };

            return detalleVentaDTO;
        }
    }
}
