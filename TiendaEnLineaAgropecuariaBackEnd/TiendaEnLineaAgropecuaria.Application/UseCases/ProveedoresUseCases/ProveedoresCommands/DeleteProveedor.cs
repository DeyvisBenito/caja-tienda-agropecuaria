using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresCommands
{
    public class DeleteProveedor
    {
        private readonly IRepositorioProveedores repositorioProveedores;

        public DeleteProveedor(IRepositorioProveedores repositorioProveedores)
        {
            this.repositorioProveedores = repositorioProveedores;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var result = await repositorioProveedores.Delete(id);

            return result;
        }
    }
}
