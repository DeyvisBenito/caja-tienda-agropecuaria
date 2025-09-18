using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.InventariosUseCases.InventariosCommands
{
    public class PutInventario
    {
        private readonly IRepositorioInventario repositorioInventario;

        public PutInventario(IRepositorioInventario repositorioInventario)
        {
            this.repositorioInventario = repositorioInventario;
        }

        public async Task<bool> ExecuteAsync(int id, InventarioCreacionDTO inventarioDTO, string urlFoto)
        {
            var inventario = new Inventario
            {
                EstadoId = inventarioDTO.EstadoId,
                TipoProductoId = inventarioDTO.TipoProductoId,
                BodegaId = inventarioDTO.BodegaId,
                Nombre = inventarioDTO.Nombre,
                Marca = inventarioDTO.Marca,
                Descripcion = inventarioDTO.Descripcion,
                Precio = inventarioDTO.Precio,
                Stock = inventarioDTO.Stock,
                UrlFoto = urlFoto
            };

            var result = await repositorioInventario.UpdateInventario(id, inventario);

            return result;
        }
    }
}
