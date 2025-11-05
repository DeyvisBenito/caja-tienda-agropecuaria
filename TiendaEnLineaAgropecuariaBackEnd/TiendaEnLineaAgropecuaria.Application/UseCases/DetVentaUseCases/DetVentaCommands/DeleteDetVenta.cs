using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.DetVentaUseCases.DetVentaCommands
{
    public class DeleteDetVenta
    {
        private readonly IRepositorioDetVenta repositorioDetVenta;

        public DeleteDetVenta(IRepositorioDetVenta repositorioDetVenta)
        {
            this.repositorioDetVenta = repositorioDetVenta;
        }

        public async Task<bool> ExecuteAsync(int id, int ventaId, int sucursalId)
        {
            var resp = await repositorioDetVenta.Delete(id, ventaId, sucursalId);

            return resp;
        }
    }
}
