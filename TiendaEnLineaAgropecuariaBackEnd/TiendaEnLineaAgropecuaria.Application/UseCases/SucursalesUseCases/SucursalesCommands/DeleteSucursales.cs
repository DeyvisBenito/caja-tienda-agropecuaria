using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.SucursalesUseCases.SucursalesCommands
{
    public class DeleteSucursales
    {
        private readonly IRepositorioSucursal repositorioSucursal;

         public DeleteSucursales(IRepositorioSucursal repositorioSucursal)
         {
             this.repositorioSucursal = repositorioSucursal;
         }

         public async Task<bool> ExecuteAsync(int id)
         {
             var result = await repositorioSucursal.DeleteSucursal(id);

             return result;
         }
    }
}
