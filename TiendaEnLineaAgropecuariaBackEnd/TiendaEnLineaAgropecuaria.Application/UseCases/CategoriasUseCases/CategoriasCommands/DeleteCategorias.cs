using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands
{
    public class DeleteCategorias
    {
        private readonly IRepositorioCategorias repositorioCategorias;

        public DeleteCategorias(IRepositorioCategorias repositorioCategorias)
        {
            this.repositorioCategorias = repositorioCategorias;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var result = await repositorioCategorias.DeleteCategoria(id);

            return result;
        }
    }
}
