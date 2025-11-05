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
    public class PostVenta
    {
        private readonly IRepositorioVentas repositorioVentas;

        public PostVenta(IRepositorioVentas repositorioVentas)
        {
            this.repositorioVentas = repositorioVentas;
        }

        public async Task<int> ExecuteAsync(VentaCreacionUserSucursalId ventaCreacionUserSucursal)
        {
            var newVenta = new Venta
            {
                UserId = ventaCreacionUserSucursal.UserId,
                SucursalId = ventaCreacionUserSucursal.SucursalId,
                Total = ventaCreacionUserSucursal.Total,
                FechaCreacion = DateTime.UtcNow
            };

            var resp = await repositorioVentas.NewVenta(newVenta, ventaCreacionUserSucursal.ClienteNit);

            return resp;
        }
    }
}
