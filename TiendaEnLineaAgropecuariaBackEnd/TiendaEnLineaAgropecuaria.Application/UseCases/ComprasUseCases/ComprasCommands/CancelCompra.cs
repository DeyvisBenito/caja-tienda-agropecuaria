using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasCommands
{
    public class CancelCompra
    {
        private readonly IRepositorioCompras repositorioCompras;

        public CancelCompra(IRepositorioCompras repositorioCompras)
        {
            this.repositorioCompras = repositorioCompras;
        }

        public async Task<bool> ExecuteAsync(int id, int sucursalId)
        {
            var resp = await repositorioCompras.CancelCompra(id, sucursalId);

            return resp;
        }
    }
}
