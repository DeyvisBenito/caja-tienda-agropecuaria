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
    public class GetAllVentas
    {
        private readonly IRepositorioVentas repositorioVentas;

        public GetAllVentas(IRepositorioVentas repositorioVentas)
        {
            this.repositorioVentas = repositorioVentas;
        }

        public async Task<List<VentaDTO>> ExecuteAsync()
        {
            var ventas = await repositorioVentas.Get();
            var ventasDTO = ventas.Select(x => new VentaDTO
            {
                UserId = x.UserId,
                Id = x.Id,
                SucursalId = x.SucursalId,
                Sucursal = x.Sucursal!.Nombre,
                EstadoId = x.EstadoId,
                Estado = x.Estado!.Nombre,
                Total = x.Total,
                FechaCreacion = x.FechaCreacion,

                Cliente = new ClienteDTO
                {
                    Id = x.ClienteId,
                    Nit = x.Cliente!.Nit,
                    Nombres = x.Cliente.Nombres,
                    Apellidos = x.Cliente.Apellidos,
                    Email = x.Cliente.Email,
                    Telefono = x.Cliente.Telefono
                },

                DetallesVenta = x.DetallesVenta!.Select(d => new DetalleVentaDTO
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
            }).ToList();

            return ventasDTO;
        }
    }
}
