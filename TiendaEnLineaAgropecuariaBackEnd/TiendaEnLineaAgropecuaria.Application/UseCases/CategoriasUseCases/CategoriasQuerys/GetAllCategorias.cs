using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys
{
    public class GetAllCategorias
    {
        private readonly IRepositorioCategorias repositorioCategorias;

        public GetAllCategorias(IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
        }

        public async Task<List<CategoriaDTO>> ExecuteAsync()
        {
            var categorias = await repositorioCategorias.GetAllCategorias();
            var categoriaDTO = categorias.Select(x => new CategoriaDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Estado = x.Estado!.Nombre,
                EstadoId = x.EstadoId
            }).ToList();

            return categoriaDTO;
        }
    }
}
