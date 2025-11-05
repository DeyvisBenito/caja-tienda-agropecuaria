using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioDetVenta
    {
        Task<bool> Delete(int id, int ventaId, int sucursalId);
        Task<IEnumerable<DetalleVenta>> Get();
        Task<DetalleVenta> GetById(int ventaId, int idDet);
        Task<DetalleVenta?> GetByInvId(int ventaId, int invId, int unidadMedidaId);
        Task<DetalleVenta?> GetByInvIdUpd(int ventaId, int invId, int unidadMedidaId, int detId);
        Task<IEnumerable<DetalleVenta>> GetByVentaId(int ventaId, int sucursalId);
        Task<bool> NewDetalle(DetalleVenta detalle, int sucursalId);
        Task<bool> UpdateDetalle(DetalleVenta detalle, int id, int idVenta, int sucursalId);
    }
}
