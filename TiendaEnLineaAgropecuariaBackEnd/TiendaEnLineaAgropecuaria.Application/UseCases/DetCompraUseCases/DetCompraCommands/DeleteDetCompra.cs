using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.DetallesCompraDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.DetCompraUseCases.DetCompraCommands
{
    public class DeleteDetCompra
    {
        private readonly IRepositorioDetCompra repositorioDetCompra;

        public DeleteDetCompra(IRepositorioDetCompra repositorioDetCompra)
        {
            this.repositorioDetCompra = repositorioDetCompra;
        }

        public async Task<bool> ExecuteAsync(int id, int compraId, int sucursalId)
        {
            var resp = await repositorioDetCompra.Delete(id, compraId, sucursalId);

            return resp;
        }
    }
}
