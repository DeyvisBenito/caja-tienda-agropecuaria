using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ConversionesUseCases.ConversionesQuerys
{
    public class GetDescuentoByConversion
    {
        private readonly IRepositorioConversiones repositorioConversiones;

        public GetDescuentoByConversion(IRepositorioConversiones repositorioConversiones)
        {
            this.repositorioConversiones = repositorioConversiones;
        }

        public async Task<decimal> ExecuteAsync(int unMeOrigenId, int unMeDestinoId)
        {
            var descuento = await repositorioConversiones.GetDescuento(unMeOrigenId, unMeDestinoId);

            return descuento;
        }
    }
}
