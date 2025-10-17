using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ClientesUseCases.ClientesCommands
{
    public class DeleteCliente
    {
        private readonly IRepositorioClientes repositorioClientes;

        public DeleteCliente(IRepositorioClientes repositorioClientes)
        {
            this.repositorioClientes = repositorioClientes;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var result = await repositorioClientes.Delete(id);

            return result;
        }
    }
}
