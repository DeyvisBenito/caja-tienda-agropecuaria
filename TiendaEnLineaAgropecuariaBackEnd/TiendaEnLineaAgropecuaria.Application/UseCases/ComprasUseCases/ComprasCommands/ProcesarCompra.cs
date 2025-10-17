using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasCommands
{
    public class ProcesarCompra
    {
        private readonly IRepositorioCompras repositorioCompras;

        public ProcesarCompra(IRepositorioCompras repositorioCompras)
        {
            this.repositorioCompras = repositorioCompras;
        }

        public async Task<bool> ExecuteAsync(int idCompra, int idSucursal)
        {
            var resp = await repositorioCompras.ProcesarCompra(idCompra, idSucursal);

            return resp;
        }
    }
}
