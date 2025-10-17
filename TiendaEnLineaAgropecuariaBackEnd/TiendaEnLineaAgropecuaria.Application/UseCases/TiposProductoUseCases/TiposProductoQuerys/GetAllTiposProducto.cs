using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.TipoProductosDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoQuerys
{
    public class GetAllTiposProducto
    {
        private readonly IRepositorioTipoProductos repositorioTipoProductos;

        public GetAllTiposProducto(IRepositorioTipoProductos repositorioTipoProductos)
        {
            this.repositorioTipoProductos = repositorioTipoProductos;
        }

        public async Task<List<TipoProductosDTO>> ExecuteAsync()
        {
            var tiposProducto = await repositorioTipoProductos.GetAllTipoProductos();
            var tiposProductoDTO = tiposProducto.Select(x => new TipoProductosDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Categoria = x.Categoria!.Nombre,
                Estado = x.Estado!.Nombre,
                CategoriaId = x.CategoriaId,
                EstadoId = x.EstadoId,
                TipoMedidaId = x.TipoMedidaId,
                TipoMedida = x.TipoMedida!.Nombre
            }).ToList();

            return tiposProductoDTO;
        }
    }
}
