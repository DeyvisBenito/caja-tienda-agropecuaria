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
    public class GetAllInventarios
    {
        private readonly IRepositorioInventario repositorioInventario;

        public GetAllInventarios(IRepositorioInventario repositorioInventario)
        {
            this.repositorioInventario = repositorioInventario;
        }

        public async Task<List<InventarioDTO>> ExecuteAsync()
        {
            var inventarios = await repositorioInventario.Get();
            var inventarioDTO = inventarios.Select(x => new InventarioDTO
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Nombre = x.Nombre,
                TipoProductoId = x.TipoProductoId,
                TipoProducto = x.TipoProducto!.Nombre,
                EstadoId = x.EstadoId,
                Estado = x.Estado!.Nombre,
                SucursalId = x.SucursalId,
                Sucursal = x.Sucursal!.Nombre,
                Marca = x.Marca,
                PrecioCostoPromedio = x.PrecioCostoPromedio,
                PrecioVenta = x.PrecioVenta,
                UrlFoto = x.UrlFoto,
                Descripcion = x.Descripcion,
                Stock = x.Stock,
                UnidadMedidaId = x.UnidadMedidaId,
                UnidadMedida = x.UnidadMedida!.Medida
            }).ToList();

            return inventarioDTO;
        } 
    }
}
