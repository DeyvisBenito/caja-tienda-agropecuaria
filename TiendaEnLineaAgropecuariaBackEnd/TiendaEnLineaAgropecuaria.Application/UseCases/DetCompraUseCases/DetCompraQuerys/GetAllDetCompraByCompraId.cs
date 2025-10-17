using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.DetCompraUseCases.DetCompraQuerys
{
    public class GetAllDetCompraByCompraId
    {
        private readonly IRepositorioDetCompra repositorioDetCompra;

        public GetAllDetCompraByCompraId(IRepositorioDetCompra repositorioDetCompra)
        {
            this.repositorioDetCompra = repositorioDetCompra;
        }

        public async Task<IEnumerable<DetalleCompraDTO>> ExecuteAsync(int compraId, int sucursalId)
        {
            var detallesCompra = await repositorioDetCompra.GetByCompraId(compraId, sucursalId);

            var detallesCompraDTO = detallesCompra.Select(x => new DetalleCompraDTO
            {
                Id = x.Id,
                Cantidad = x.Cantidad,
                CompraId = x.CompraId,
                Estado = x.Estado!.Nombre,
                InventarioId = x.InventarioId,
                UnidadMedidaId = x.UnidadMedidaId,
                UnidadMedida = x.UnidadMedida!.Medida,
                EstadoId = x.EstadoId,
                Fecha = x.Fecha,
                PrecioCosto = x.PrecioCosto,
                UnidadesPorCaja = x.UnidadesPorCaja,

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

            return detallesCompraDTO;
        }
    }
}
