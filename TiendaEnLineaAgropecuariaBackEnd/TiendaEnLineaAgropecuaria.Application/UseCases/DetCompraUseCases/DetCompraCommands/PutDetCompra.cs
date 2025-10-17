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
    public class PutDetCompra
    {
        private readonly IRepositorioDetCompra repositorioDetCompra;

        public PutDetCompra(IRepositorioDetCompra repositorioDetCompra)
        {
            this.repositorioDetCompra = repositorioDetCompra;
        }

        public async Task<bool> ExecuteAsync(DetalleCompraCreacionDTO detalleCompraCreacionDTO, int id, int idCompra, int sucursalId)
        {
            var detalle = new DetalleCompra
            {
                Cantidad = detalleCompraCreacionDTO.Cantidad,
                CompraId = detalleCompraCreacionDTO.CompraId,
                EstadoId = detalleCompraCreacionDTO.EstadoId,
                InventarioId = detalleCompraCreacionDTO.InventarioId,
                PrecioCosto = detalleCompraCreacionDTO.PrecioCosto,
                UnidadMedidaId = detalleCompraCreacionDTO.UnidadMedidaId,
                UnidadesPorCaja = detalleCompraCreacionDTO.UnidadesPorCaja,
                
            };
            var resp = await repositorioDetCompra.UpdateDetalle(detalle, id, idCompra, sucursalId);

            return resp;
        }
    }
}
