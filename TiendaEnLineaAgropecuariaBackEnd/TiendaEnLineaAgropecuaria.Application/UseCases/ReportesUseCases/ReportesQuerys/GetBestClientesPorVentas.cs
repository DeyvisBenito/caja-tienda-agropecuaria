using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ReportesUseCases.ReportesQuerys
{
    public class GetBestClientesPorVentas
    {
        private readonly IRepositorioReportes repositorioReportes;

        public GetBestClientesPorVentas(IRepositorioReportes repositorioReportes)
        {
            this.repositorioReportes = repositorioReportes;
        }

        public async Task<IEnumerable<object>> ExecuteAsync()
        {
            var clientes = await repositorioReportes.BestClientesPorVentas();

            return clientes;
        }
    }
}
