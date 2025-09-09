using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands
{
    public class PostCategoria
    {
        private readonly IRepositorioCategorias repositorioCategorias;

        public PostCategoria(IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
        }

        public async Task<bool> ExecuteAsync(CategoriaCreacionConUserIdDTO categoriaDTO)
        {
            Categoria categoria = new Categoria
            {
                IdUser = categoriaDTO.UserId,
                EstadoId = categoriaDTO.IdEstado,
                Nombre = categoriaDTO.Nombre
            };

            var result = await repositorioCategorias.NewCategoria(categoria);

            return result;
        }
    }
}
