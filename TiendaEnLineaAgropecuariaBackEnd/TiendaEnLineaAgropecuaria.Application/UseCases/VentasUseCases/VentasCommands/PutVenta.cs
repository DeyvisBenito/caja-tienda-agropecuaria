using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.VentasDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.VentasUseCases.VentasCommands
{
    public class PutVenta
    {
        private readonly IRepositorioVentas repositorioVentas;

        public PutVenta(IRepositorioVentas repositorioVentas)
        {
            this.repositorioVentas = repositorioVentas;
        }

        public async Task<bool> ExecuteAsync(int id, VentaUpdateDTO ventaUpdateDTO, int sucursalId)
        {
            var nuevoNit = ventaUpdateDTO.NitCliente;
            var ventaUpdate = new Venta
            {
                UserId = string.Empty,
                SucursalId = sucursalId,
            };
            var resp = await repositorioVentas.Update(id, ventaUpdate, nuevoNit);

            return resp;
        }
    }
}
