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
    public class GetCategoriaById
    {
        private readonly IRepositorioCategorias repositorioCategorias;

        public GetCategoriaById(IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
        }

        public async Task<CategoriaDTO> ExecuteAsync(int id)
        {
            var categoria = await repositorioCategorias.GetCategoria(id);

            var categoriaDTO = new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Estado = categoria.Estado!.Nombre,
                EstadoId = categoria.EstadoId      
            };

            return categoriaDTO;
        }
    }
}
