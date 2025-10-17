using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.DetCompraUseCases.DetCompraQuerys
{
    public class GetDetByInvId
    {
        private readonly IRepositorioDetCompra repositorioDetCompra;

        public GetDetByInvId(IRepositorioDetCompra repositorioDetCompra)
        {
            this.repositorioDetCompra = repositorioDetCompra;
        }

        public async Task<DetalleCompra?> ExecuteAsync(int compraId, int invId)
        {
            var det = await repositorioDetCompra.GetByInvId(compraId, invId);

            return det;
        }
    }
}
