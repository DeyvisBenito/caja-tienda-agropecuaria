using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.DetVentaUseCases.DetVentaQuerys
{
    public class GetDetVentaByInvId
    {
        private readonly IRepositorioDetVenta repositorioDetVenta;

        public GetDetVentaByInvId(IRepositorioDetVenta repositorioDetVenta)
        {
            this.repositorioDetVenta = repositorioDetVenta;
        }

        public async Task<DetalleVenta?> ExecuteAsync(int ventaId, int invId, int unidadMedidaId)
        {
            var det = await repositorioDetVenta.GetByInvId(ventaId, invId, unidadMedidaId);

            return det;
        }
    }
}
