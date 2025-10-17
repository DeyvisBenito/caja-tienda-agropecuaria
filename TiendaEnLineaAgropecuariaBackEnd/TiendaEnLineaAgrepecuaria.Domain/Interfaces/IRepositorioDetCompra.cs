using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioDetCompra
    {
        Task<bool> Delete(int id, int compraId, int sucursalId);
        Task<IEnumerable<DetalleCompra>> Get();
        Task<IEnumerable<DetalleCompra>> GetByCompraId(int compraId, int sucursalId);
        Task<DetalleCompra> GetById(int idCompra, int idDet);
        Task<DetalleCompra?> GetByInvId(int compraId, int invId);
        Task<bool> NewDetalle(DetalleCompra detalle, int sucursalId);
        Task<bool> UpdateDetalle(DetalleCompra detalle, int id, int idCompra, int sucursalId);
    }
}
