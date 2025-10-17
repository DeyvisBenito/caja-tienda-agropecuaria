using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioCompras
    {
        Task<bool> CancelCompra(int id, int sucursalId);
        Task<bool> ProcesarCompra(int idCompra, int idSucursal);
        Task<bool> Delete(int id);
        Task<List<Compra>> Get();
        Task<Compra> GetById(int id);
        Task<Compra> GetCompraPendiente(int sucursalId);
        Task<int> NewCompra(Compra compra, string proveedorNit);
        Task<bool> Update(int id, Compra compra, string nuevoNit);
    }
}
