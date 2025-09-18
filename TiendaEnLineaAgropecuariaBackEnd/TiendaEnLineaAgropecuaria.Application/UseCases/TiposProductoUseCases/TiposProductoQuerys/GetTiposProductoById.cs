using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.TipoProductosDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoQuerys
{
    public class GetTiposProductoById
    {
        private readonly IRepositorioTipoProductos repositorioTipoProductos;

        public GetTiposProductoById(IRepositorioTipoProductos repositorioTipoProductos)
        {
            this.repositorioTipoProductos = repositorioTipoProductos;
        }

        public async Task<TipoProductosDTO> ExecuteAsync(int id)
        {
            var tipoProducto = await repositorioTipoProductos.GetTipoProducto(id);

            var tipoProductoDTO = new TipoProductosDTO
            {
                Id = tipoProducto.Id,
                Nombre = tipoProducto.Nombre,
                Categoria = tipoProducto.Categoria!.Nombre,
                Estado = tipoProducto.Estado!.Nombre,
                EstadoId = tipoProducto.EstadoId,
                CategoriaId = tipoProducto.CategoriaId
            };

            return tipoProductoDTO;
        }
    }
}
