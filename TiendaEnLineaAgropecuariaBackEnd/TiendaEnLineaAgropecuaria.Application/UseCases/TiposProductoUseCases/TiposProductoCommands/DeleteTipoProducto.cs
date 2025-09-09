using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoCommands
{
    public class DeleteTipoProducto
    {
        private readonly IRepositorioTipoProductos repositorioTipoProductos;

        public DeleteTipoProducto(IRepositorioTipoProductos repositorioTipoProductos)
        {
            this.repositorioTipoProductos = repositorioTipoProductos;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var result = await repositorioTipoProductos.DeleteTipoProducto(id);

            return result;
        }
    }
}
