using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioVentas
    {
        Task<bool> CancelVenta(int id, int sucursalId);
        Task<bool> Delete(int id);
        Task<List<Venta>> Get();
        Task<Venta> GetById(int id);
        Task<int> NewVenta(Venta venta, string clienteNit);
        Task<decimal> ProcesarVenta(int idVenta, int idSucursal, string userId, int tipoPagoId, decimal pago);
        Task<bool> Update(int id, Venta venta, string nuevoNit);
    }
}
