using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioProveedores
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<Proveedor>> Get();
        Task<Proveedor> Get(int id);
        Task<bool> NewProveedor(Proveedor proveedor);
        Task<bool> Update(int id, Proveedor proveedor);
    }
}
