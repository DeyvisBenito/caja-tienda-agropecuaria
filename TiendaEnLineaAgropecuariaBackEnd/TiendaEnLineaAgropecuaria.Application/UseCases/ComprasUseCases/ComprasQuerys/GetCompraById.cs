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
    public class GetCompraById
    {
        private readonly IRepositorioCompras repositorioCompras;

        public GetCompraById(IRepositorioCompras repositorioCompras)
        {
            this.repositorioCompras = repositorioCompras;
        }

        public async Task<CompraDTO> ExecuteAsync(int id)
        {
            var compra = await repositorioCompras.GetById(id);
            var comprasDTO = new CompraDTO
            {
                UserId = compra.IdUser,
                Id = compra.Id,
                SucursalId = compra.SucursalId,
                Sucursal = compra.Sucursal!.Nombre,
                EstadoId = compra.EstadoId,
                Estado = compra.Estado!.Nombre,
                Total = compra.Total,
                FechaCreacion = compra.FechaCreacion,

                Proveedor = new ProveedorDTO
                {
                    Id = compra.ProveedorId,
                    Nit = compra.Proveedor!.Nit,
                    Nombres = compra.Proveedor.Nombres,
                    Apellidos = compra.Proveedor.Apellidos,
                    Telefono = compra.Proveedor.Telefono,
                    Ubicacion = compra.Proveedor.Ubicacion,
                    EstadoId = compra.Proveedor.EstadoId,
                    Estado = compra.Proveedor.Estado!.Nombre
                },

                DetallesCompra = compra.DetallesCompra!.Select(d => new DetalleCompraDTO
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
            };
            return comprasDTO;
        }
    }
}
