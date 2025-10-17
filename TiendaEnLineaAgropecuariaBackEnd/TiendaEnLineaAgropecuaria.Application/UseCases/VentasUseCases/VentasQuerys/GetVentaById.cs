using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.ClientesDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesVentaDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.VentasDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.VentasUseCases.VentasQuerys
{
    public class GetVentaById
    {
        private readonly IRepositorioVentas repositorioVentas;

        public GetVentaById(IRepositorioVentas repositorioVentas)
        {
            this.repositorioVentas = repositorioVentas;
        }

        public async Task<VentaDTO> ExecuteAsync(int id)
        {
            var venta = await repositorioVentas.GetById(id);
            var ventaDTO = new VentaDTO
            {
                UserId = venta.UserId,
                Id = venta.Id,
                SucursalId = venta.SucursalId,
                Sucursal = venta.Sucursal!.Nombre,
                EstadoId = venta.EstadoId,
                Estado = venta.Estado!.Nombre,
                Total = venta.Total,
                FechaCreacion = venta.FechaCreacion,
                ClienteId = venta.ClienteId,

                Cliente = new ClienteDTO
                {
                    Id = venta.ClienteId,
                    Nit = venta.Cliente!.Nit,
                    Nombres = venta.Cliente.Nombres,
                    Apellidos = venta.Cliente.Apellidos,
                    Email = venta.Cliente.Email,
                    Telefono = venta.Cliente.Telefono,
                    FechaRegistro = venta.Cliente.FechaRegistro
                },

                DetallesVenta = venta.DetallesVenta!.Select(d => new DetalleVentaDTO
                {
                    Id = d.Id,
                    VentaId = d.VentaId,
                    EstadoId = d.EstadoId,
                    Estado = d.Estado!.Nombre,
                    UnidadMedidaId = d.UnidadMedidaId,
                    UnidadMedida = d.UnidadMedida!.Medida,
                    InventarioId = d.InventarioId,
                    PrecioVenta = d.PrecioVenta,
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
            return ventaDTO;
        }
    }
}
