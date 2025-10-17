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
                Codigo = inventarioDTO.Codigo,
                IdUser = inventarioDTO.IdUser,
                Marca = inventarioDTO.Marca,
                Nombre = inventarioDTO.Nombre,
                UrlFoto = inventarioDTO.UrlFoto,
                Descripcion = inventarioDTO.Descripcion,
                EstadoId = inventarioDTO.EstadoId,
                SucursalId = inventarioDTO.SucursalId,
                TipoProductoId = inventarioDTO.TipoProductoId,
                UnidadMedidaId = inventarioDTO.UnidadMedidaId,
                PrecioCostoPromedio = 0,
                PrecioVenta = 0,
                Stock = 0
            };

            var result = await repositorioInventario.NewInventario(inventario);

            return result;
        } 
    }
}
