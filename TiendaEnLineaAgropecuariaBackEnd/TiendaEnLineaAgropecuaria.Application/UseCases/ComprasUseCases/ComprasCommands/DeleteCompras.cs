using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasCommands
{
    public class DeleteCompras
    {
        private readonly IRepositorioCompras repositorioCompras;

        public DeleteCompras(IRepositorioCompras repositorioCompras)
        {
            this.repositorioCompras = repositorioCompras;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var resp = await repositorioCompras.Delete(id);

            return resp;
        }
    }
}
