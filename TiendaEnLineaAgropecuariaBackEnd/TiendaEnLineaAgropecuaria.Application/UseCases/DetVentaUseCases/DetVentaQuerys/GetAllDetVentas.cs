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
    public class GetAllDetVentas
    {
        private readonly IRepositorioDetVenta repositorioDetVenta;

        public GetAllDetVentas(IRepositorioDetVenta repositorioDetVenta)
        {
            this.repositorioDetVenta = repositorioDetVenta;
        }

        public async Task<IEnumerable<DetalleVentaDTO>> ExecuteAsync()
        {
            var detallesVenta = await repositorioDetVenta.Get();

            var detallesVentaDTO = detallesVenta.Select(x => new DetalleVentaDTO
            {
                Id = x.Id,
                Cantidad = x.Cantidad,
                VentaId = x.VentaId,
                Estado = x.Estado!.Nombre,
                InventarioId = x.InventarioId,
                UnidadMedidaId = x.UnidadMedidaId,
                UnidadMedida = x.UnidadMedida!.Medida,
                EstadoId = x.EstadoId,
                Fecha = x.Fecha,
                PrecioVenta = x.PrecioVentaUnidadMinima,
                UnidadesPorCaja = x.UnidadesPorCaja,
                Descuento = x.Descuento,
                PrecioVentaConDescuento = x.PrecioVentaConDescuentoUnidadMinima,
                Total = x.Total,

                Inventario = new InventarioDTO
                {
                    Id = x.Inventario!.Id,
                    Codigo = x.Inventario.Codigo,
                    Descripcion = x.Inventario.Descripcion,
                    EstadoId = x.Inventario.EstadoId,
                    Estado = x.Inventario.Estado!.Nombre,
                    Marca = x.Inventario.Marca,
                    Nombre = x.Inventario.Nombre,
                    Sucursal = x.Inventario.Sucursal!.Nombre,
                    SucursalId = x.Inventario.SucursalId,
                    TipoProductoId = x.Inventario.TipoProductoId,
                    TipoProducto = x.Inventario.TipoProducto!.Nombre,
                    UnidadMedida = x.Inventario.UnidadMedida!.Medida,
                    UnidadMedidaId = x.Inventario.UnidadMedidaId,
                    PrecioCostoPromedio = x.Inventario.PrecioCostoPromedio,
                    PrecioVenta = x.Inventario.PrecioVenta,
                    UrlFoto = x.Inventario.UrlFoto,
                    Stock = x.Inventario.Stock
                }
            }).ToList();

            return detallesVentaDTO;
        }
    }
}
