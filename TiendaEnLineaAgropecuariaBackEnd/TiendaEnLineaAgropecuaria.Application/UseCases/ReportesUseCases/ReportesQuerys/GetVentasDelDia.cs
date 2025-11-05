using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ReportesUseCases.ReportesQuerys
{
    public class GetVentasDelDia
    {
        private readonly IRepositorioReportes repositorioReportes;

        public GetVentasDelDia(IRepositorioReportes repositorioReportes)
        {
            this.repositorioReportes = repositorioReportes;
        }

        public async Task<IEnumerable<object>> ExecuteAsync()
        {
            var ventas = await repositorioReportes.VentasDelDia();

            return ventas;
        }
    }
}
