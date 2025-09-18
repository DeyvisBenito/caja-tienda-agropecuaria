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
    public class PostInventario
    {
        private readonly IRepositorioInventario repositorioInventario;

        public PostInventario(IRepositorioInventario repositorioInventario)
        {
            this.repositorioInventario = repositorioInventario;
        }

        public async Task<bool> ExecuteAsync(InventarioCreacionConUserIdDTO inventarioDTO)
        {
            var inventario = new Inventario
            {
                IdUser = inventarioDTO.IdUser,
                EstadoId = inventarioDTO.EstadoId,
                TipoProductoId = inventarioDTO.TipoProductoId,
                BodegaId = inventarioDTO.BodegaId,
                Nombre = inventarioDTO.Nombre,
                Marca = inventarioDTO.Marca,
                Descripcion = inventarioDTO.Descripcion,
                Precio = inventarioDTO.Precio,
                Stock = inventarioDTO.Stock,
                UrlFoto = inventarioDTO.UrlFoto
            };

            var result = await repositorioInventario.NewInventario(inventario);

            return result;
        }
    }
}
