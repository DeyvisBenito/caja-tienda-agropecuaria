using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ReportesUseCases.ReportesQuerys
{
    public class GetBestProveedores
    {
        private readonly IRepositorioReportes repositorioReportes;

        public GetBestProveedores(IRepositorioReportes repositorioReportes)
        {
            this.repositorioReportes = repositorioReportes;
        }

        public async Task<IEnumerable<object>> ExecuteAsync()
        {
            var proveedores = await repositorioReportes.BestProveedores();

            return proveedores;
        }
    }
}
