using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.InventariosUseCases.InventariosQuerys
{
    public class GetInventarioById
    {
        private readonly IRepositorioInventario repositorioInventario;

        public GetInventarioById(IRepositorioInventario repositorioInventario)
        {
            this.repositorioInventario = repositorioInventario;
        }

        public async Task<InventarioDTO> ExecuteAsync(int id)
        {
            var inventario = await repositorioInventario.GetById(id);

            var inventarioDTO = new InventarioDTO
            {
                Id = inventario.Id,
                Codigo = inventario.Codigo,
                Nombre = inventario.Nombre,
                TipoProductoId = inventario.TipoProductoId,
                TipoProducto = inventario.TipoProducto!.Nombre,
                EstadoId = inventario.EstadoId,
                Estado = inventario.Estado!.Nombre,
                SucursalId = inventario.SucursalId,
                Sucursal = inventario.Sucursal!.Nombre,
                Marca = inventario.Marca,
                PrecioCostoPromedio = inventario.PrecioCostoPromedio,
                PrecioVenta = inventario.PrecioVenta,
                UrlFoto = inventario.UrlFoto,
                Descripcion = inventario.Descripcion,
                Stock = inventario.Stock,
                UnidadMedidaId = inventario.UnidadMedidaId,
                UnidadMedida = inventario.UnidadMedida!.Medida
            };

            return inventarioDTO;
        }
    }
}
