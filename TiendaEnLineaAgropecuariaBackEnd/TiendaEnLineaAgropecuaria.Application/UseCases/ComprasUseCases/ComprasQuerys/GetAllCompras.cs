using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasQuerys
{
    public class GetAllCompras
    {
        private readonly IRepositorioCompras repositorioCompras;

        public GetAllCompras(IRepositorioCompras repositorioCompras)
        {
            this.repositorioCompras = repositorioCompras;
        }

        public async Task<List<CompraDTO>> ExecuteAsync()
        {
            var compras = await repositorioCompras.Get();
            var comprasDTO = compras.Select(x => new CompraDTO
            {
                UserId = x.IdUser,
                Id = x.Id,
                SucursalId = x.SucursalId,
                Sucursal = x.Sucursal!.Nombre,
                EstadoId = x.EstadoId,
                Estado = x.Estado!.Nombre,
                Total = x.Total,
                FechaCreacion = x.FechaCreacion,

                Proveedor = new ProveedorDTO
                {
                    Id = x.ProveedorId,
                    Nit = x.Proveedor!.Nit,
                    Nombres = x.Proveedor.Nombres,
                    Apellidos = x.Proveedor.Apellidos,
                    Telefono = x.Proveedor.Telefono,
                    Ubicacion = x.Proveedor.Ubicacion,
                    EstadoId = x.Proveedor.EstadoId,
                    Estado = x.Proveedor.Estado!.Nombre
                },

                DetallesCompra = x.DetallesCompra!.Select(d => new DetalleCompraDTO
                {
                    Id = d.Id,
                    CompraId = d.CompraId,
                    EstadoId = d.EstadoId,
                    Estado = d.Estado!.Nombre,
                    UnidadMedidaId = d.UnidadMedidaId,
                    UnidadMedida = d.UnidadMedida!.Medida,
                    InventarioId = d.InventarioId,
                    PrecioCosto = d.PrecioCosto,
                    Cantidad = d.Cantidad,
                    Fecha = d.Fecha,
                    UnidadesPorCaja = d.UnidadesPorCaja,

                    Inventario = new InventarioDTO
                    {
                        Id = d.Inventario!.Id,
                        Codigo = d.Inventario.Codigo,
                        Nombre = d.Inventario.Nombre,
                        TipoProductoId = d.Inventario.TipoProductoId,
                        TipoProducto = d.Inventario.TipoProducto!.Nombre,
                        EstadoId = d.Inventario.EstadoId,
                        Estado = d.Inventario.Estado!.Nombre,
                        SucursalId = d.Inventario.SucursalId,
                        Sucursal = d.Inventario.Sucursal!.Nombre,
                        Marca = d.Inventario.Marca,
                        PrecioCostoPromedio = d.Inventario.PrecioCostoPromedio,
                        PrecioVenta = d.Inventario.PrecioVenta,
                        UrlFoto = d.Inventario.UrlFoto,
                        Descripcion = d.Inventario.Descripcion,
                        Stock = d.Inventario.Stock,
                        UnidadMedidaId = d.Inventario.UnidadMedidaId,
                        UnidadMedida = d.Inventario.UnidadMedida!.Medida
                    }
                }).ToList()
            }).ToList();

            return comprasDTO;
        }
    }
}
