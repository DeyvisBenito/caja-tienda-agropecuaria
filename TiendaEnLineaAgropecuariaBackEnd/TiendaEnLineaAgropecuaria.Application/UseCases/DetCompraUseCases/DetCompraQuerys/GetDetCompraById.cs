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
    public class GetDetCompraById
    {
        private readonly IRepositorioDetCompra repositorioDetCompra;

        public GetDetCompraById(IRepositorioDetCompra repositorioDetCompra)
        {
            this.repositorioDetCompra = repositorioDetCompra;
        }

        public async Task<DetalleCompraDTO> ExecuteAsync(int idCompra, int idDet)
        {
            var detalleCompra = await repositorioDetCompra.GetById(idCompra, idDet);

            var detalleCompraDTO = new DetalleCompraDTO
            {
                Id = detalleCompra.Id,
                Cantidad = detalleCompra.Cantidad,
                CompraId = detalleCompra.CompraId,
                Estado = detalleCompra.Estado!.Nombre,
                InventarioId = detalleCompra.InventarioId,
                UnidadMedidaId = detalleCompra.UnidadMedidaId,
                UnidadMedida = detalleCompra.UnidadMedida!.Medida,
                EstadoId = detalleCompra.EstadoId,
                Fecha = detalleCompra.Fecha,
                PrecioCosto = detalleCompra.PrecioCosto,
                UnidadesPorCaja = detalleCompra.UnidadesPorCaja,

                Inventario = new InventarioDTO
                {
                    Id = detalleCompra.Inventario!.Id,
                    Codigo = detalleCompra.Inventario.Codigo,
                    Descripcion = detalleCompra.Inventario.Descripcion,
                    EstadoId = detalleCompra.Inventario.EstadoId,
                    Estado = detalleCompra.Inventario.Estado!.Nombre,
                    Marca =detalleCompra.Inventario.Marca,
                    Nombre = detalleCompra.Inventario.Nombre,
                    Sucursal = detalleCompra.Inventario.Sucursal!.Nombre,
                    SucursalId = detalleCompra.Inventario.SucursalId,
                    TipoProductoId = detalleCompra.Inventario.TipoProductoId,
                    TipoProducto = detalleCompra.Inventario.TipoProducto!.Nombre,
                    UnidadMedida = detalleCompra.Inventario.UnidadMedida!.Medida,
                    UnidadMedidaId = detalleCompra.Inventario.UnidadMedidaId,
                    PrecioCostoPromedio = detalleCompra.Inventario.PrecioCostoPromedio,
                    PrecioVenta = detalleCompra.Inventario.PrecioVenta,
                    UrlFoto =detalleCompra.Inventario.UrlFoto,
                    Stock = detalleCompra.Inventario.Stock
                }
            };

            return detalleCompraDTO;
        }
    }
}
