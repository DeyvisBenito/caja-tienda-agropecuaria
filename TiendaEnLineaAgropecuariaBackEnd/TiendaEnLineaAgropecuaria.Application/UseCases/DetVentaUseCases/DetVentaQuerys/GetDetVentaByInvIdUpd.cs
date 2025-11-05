using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.DetVentaUseCases.DetVentaQuerys
{
    public class GetDetVentaByInvIdUpd
    {
        private readonly IRepositorioDetVenta repositorioDetVenta;

        public GetDetVentaByInvIdUpd(IRepositorioDetVenta repositorioDetVenta)
        {
            this.repositorioDetVenta = repositorioDetVenta;
        }

        public async Task<DetalleVenta?> ExecuteAsync(int ventaId, int invId, int unidadMedidaId, int detId)
        {
            var det = await repositorioDetVenta.GetByInvIdUpd(ventaId, invId, unidadMedidaId, detId);

            return det;
        }
    }
}
