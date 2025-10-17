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
    public class PutCompras
    {
        private readonly IRepositorioCompras repositorioCompras;

        public PutCompras(IRepositorioCompras repositorioCompras)
        {
            this.repositorioCompras = repositorioCompras;
        }

        public async Task<bool> ExecuteAsync(int id, CompraUpdateDTO compraCreacionDTO, int sucursalId)
        {
            var nuevoNit = compraCreacionDTO.NitProveedor;
            var compraUpdate = new Compra
            {
                IdUser = string.Empty,
                SucursalId = sucursalId
            };
            var resp = await repositorioCompras.Update(id, compraUpdate, nuevoNit);

            return resp;
        }
    }
}
