using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.InventariosUseCases.InventariosCommands
{
    public class DeleteInventario
    {
        private readonly IRepositorioInventario repositorioInventario;

        public DeleteInventario(IRepositorioInventario repositorioInventario)
        {
            this.repositorioInventario = repositorioInventario;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var result = await repositorioInventario.DeleteInventario(id);

            return result;
        }
    }
}
