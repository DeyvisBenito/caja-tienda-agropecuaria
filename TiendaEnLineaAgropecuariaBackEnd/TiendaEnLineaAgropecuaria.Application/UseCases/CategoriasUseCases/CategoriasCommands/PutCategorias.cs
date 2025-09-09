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
    public class PutCategorias
    {
        private readonly IRepositorioCategorias repositorioCategorias;

        public PutCategorias(IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
        }

        public async Task<bool> ExecuteAsync(int id, CategoriaCreacionConUserIdDTO categoriaCreacionDTO)
        {
            var categoria = new Categoria
            {
                IdUser = categoriaCreacionDTO.UserId,
                EstadoId = categoriaCreacionDTO.IdEstado,
                Nombre = categoriaCreacionDTO.Nombre
            };

            var result = await repositorioCategorias.UpdateCategoria(id, categoria);

            return result;
        }
    }
}
