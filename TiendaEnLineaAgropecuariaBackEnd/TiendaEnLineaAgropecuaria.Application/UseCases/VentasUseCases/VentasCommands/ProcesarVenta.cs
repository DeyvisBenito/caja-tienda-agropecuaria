using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.VentasDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.VentasUseCases.VentasCommands
{
    public class ProcesarVenta
    {
        private readonly IRepositorioVentas repositorioVentas;

        public ProcesarVenta(IRepositorioVentas repositorioVentas)
        {
            this.repositorioVentas = repositorioVentas;
        }

        public async Task<VueltoDTO> ExecuteAsync(int idVenta, int idSucursal, string userId, int tipoPagoId, PagoDTO pago)
        {
            var vuelto = await repositorioVentas.ProcesarVenta(idVenta, idSucursal, userId, tipoPagoId, pago.Pago);

            var vueltoDTO = new VueltoDTO
            {
                Vuelto = vuelto
            };
            return vueltoDTO;
        }
    }
}
