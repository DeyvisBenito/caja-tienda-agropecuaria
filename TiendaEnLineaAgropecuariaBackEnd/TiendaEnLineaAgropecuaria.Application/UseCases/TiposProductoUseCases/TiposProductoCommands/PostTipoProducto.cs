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
    public class PostTipoProducto
    {
        private readonly IRepositorioTipoProductos repositorioTipoProductos;

        public PostTipoProducto(IRepositorioTipoProductos repositorioTipoProductos)
        {
            this.repositorioTipoProductos = repositorioTipoProductos;
        }

        public async Task<bool> ExecuteAsync(TipoProductoCreacionConUserIdDTO tipoProductoDTO)
        {
            var tipoProducto = new TipoProducto
            {
                IdUser = tipoProductoDTO.IdUser,
                Nombre = tipoProductoDTO.Nombre,
                EstadoId = tipoProductoDTO.EstadoId,
                CategoriaId = tipoProductoDTO.CategoriaId,
                TipoMedidaId = tipoProductoDTO.TipoMedidaId
            };

            var result = await repositorioTipoProductos.NewTipoProducto(tipoProducto);

            return result;
        }
    }
}
