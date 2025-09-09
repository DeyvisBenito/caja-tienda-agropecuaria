using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.TipoProductosDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoCommands
{
    public class PutTipoProducto
    {
        private readonly IRepositorioTipoProductos repositorioTipoProductos;

        public PutTipoProducto(IRepositorioTipoProductos repositorioTipoProductos)
        {
            this.repositorioTipoProductos = repositorioTipoProductos;
        }

        public async Task<bool> ExecuteAsync(int id, TipoProductoCreacionConUserIdDTO tipoProductoDTO)
        {
            var tipoProducto = new TipoProducto
            {
                IdUser = tipoProductoDTO.IdUser,
                Nombre = tipoProductoDTO.Nombre,
                CategoriaId = tipoProductoDTO.CategoriaId,
                EstadoId = tipoProductoDTO.EstadoId
            };

            var result = await repositorioTipoProductos.UpdateTipoProducto(id, tipoProducto);

            return result;
        }
        
    }
}
