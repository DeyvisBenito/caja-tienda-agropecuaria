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
            var inventarios = await repositorioInventario.GetAllInventarios();
            var inventarioDTO = inventarios.Select(x => new InventarioDTO
            {
                Id = x.Id,
                Bodega = x.Bodega!.Nombre,
                Estado = x.Estado!.Nombre,
                Marca = x.Marca,
                Nombre = x.Nombre,
                TipoProducto = x.TipoProducto!.Nombre,
                UrlFoto = x.UrlFoto,
                BodegaId = x.BodegaId,
                Descripcion = x.Descripcion,
                EstadoId = x.EstadoId,
                Precio = x.Precio,
                Stock = x.Stock,
                TipoProductoId = x.TipoProductoId
            }).ToList();

            return inventarioDTO;
        }
    }
}
