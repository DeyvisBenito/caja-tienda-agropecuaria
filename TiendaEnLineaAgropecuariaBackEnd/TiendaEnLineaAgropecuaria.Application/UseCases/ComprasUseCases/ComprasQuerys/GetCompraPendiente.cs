using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasQuerys
{
    public class GetCompraPendiente
    {
        private readonly IRepositorioCompras repositorioCompras;

        public GetCompraPendiente(IRepositorioCompras repositorioCompras)
        {
            this.repositorioCompras = repositorioCompras;
        }

        public async Task<Compra> ExecuteAsync(int sucursalId)
        {
            var compraPendiente = await repositorioCompras.GetCompraPendiente(sucursalId);

            return compraPendiente;
        }
    }
}
