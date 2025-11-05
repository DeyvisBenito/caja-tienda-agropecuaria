using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.VentasUseCases.VentasCommands
{
    public class CancelVenta
    {
        private readonly IRepositorioVentas repositorioVentas;

        public CancelVenta(IRepositorioVentas repositorioVentas)
        {
            this.repositorioVentas = repositorioVentas;
        }

        public async Task<bool> ExecuteAsync(int id, int sucursalId)
        {
            var resp = await repositorioVentas.CancelVenta(id, sucursalId);

            return resp;
        }
    }
}
