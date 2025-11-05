using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioConversiones
{
    public class RepositorioConversiones: IRepositorioConversiones
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioConversiones(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<decimal> GetDescuento(int unMeOrigenId, int unMeDestinoId)
        {
            var unidadMedidaOrigen = await dbContext.UnidadesMedida.FirstOrDefaultAsync(x => x.Id == unMeOrigenId);
            var unidadMedidaDestino = await dbContext.UnidadesMedida.FirstOrDefaultAsync(x => x.Id == unMeDestinoId);
            if(unidadMedidaOrigen is null || unidadMedidaDestino is null)
            {
                throw new KeyNotFoundException("La unidad de medida que se desea convertir no existe");
            }

            var conversion = await dbContext.Conversiones
                            .FirstOrDefaultAsync(x => x.UnidadMedidaOrigenId == unMeOrigenId && x.UnidadMedidaDestinoId == unMeDestinoId);

            if(conversion is null)
            {
                throw new KeyNotFoundException("La conversión deseada por realizar no existe");
            }
            if (conversion!.ListaPrecioId is null)
            {
                throw new KeyNotFoundException("El descuento deseado por realizar no existe");
            }
            var descuento = await dbContext.ListaPrecios.FirstOrDefaultAsync(x => x.Id == conversion.ListaPrecioId);
            if(descuento is null)
            {
                throw new KeyNotFoundException("El descuento deseado por realizar no existe");
            }

            return descuento.DescuentoPorcentaje;
        }
    }
}
