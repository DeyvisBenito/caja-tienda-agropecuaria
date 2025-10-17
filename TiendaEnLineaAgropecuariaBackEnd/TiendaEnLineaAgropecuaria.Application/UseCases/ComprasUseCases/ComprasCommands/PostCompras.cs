using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.ComprasDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasCommands
{
    public class PostCompras
    {
        private readonly IRepositorioCompras repositorioCompras;

        public PostCompras(IRepositorioCompras repositorioCompras)
        {
            this.repositorioCompras = repositorioCompras;
        }

        public async Task<int> ExecuteAsync(CompraCreacionUserSucursalId compraCreacionConUserIdDTO)
        {
            var newCompra = new Compra
            {
                IdUser = compraCreacionConUserIdDTO.UserId,
                SucursalId = compraCreacionConUserIdDTO.SucursalId,
                Total = compraCreacionConUserIdDTO.Total,
                FechaCreacion = DateTime.UtcNow
            };

            var resp = await repositorioCompras.NewCompra(newCompra, compraCreacionConUserIdDTO.ProveedorNit);

            return resp;
        }
    }
}
