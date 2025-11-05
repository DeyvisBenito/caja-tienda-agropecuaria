using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.VentasUseCases.VentasCommands
{
    public class DeleteVenta
    {
        private readonly IRepositorioVentas repositorioVentas;

        public DeleteVenta(IRepositorioVentas repositorioVentas)
        {
            this.repositorioVentas = repositorioVentas;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var resp = await repositorioVentas.Delete(id);

            return resp;
        }
    }
}
