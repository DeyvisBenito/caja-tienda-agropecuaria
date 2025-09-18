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
            var inventario = await repositorioInventario.GetInventario(id);

            var inventarioDTO = new InventarioDTO
            {
                Id = inventario.Id,
                Bodega = inventario.Bodega!.Nombre,
                Estado = inventario.Estado!.Nombre,
                Marca = inventario.Marca,
                Nombre = inventario.Nombre,
                TipoProducto = inventario.TipoProducto!.Nombre,
                UrlFoto = inventario.UrlFoto,
                BodegaId = inventario.BodegaId,
                Descripcion = inventario.Descripcion,
                EstadoId = inventario.EstadoId,
                Precio = inventario.Precio,
                Stock = inventario.Stock,
                TipoProductoId = inventario.TipoProductoId
            };

            return inventarioDTO;
        }
    }
}
