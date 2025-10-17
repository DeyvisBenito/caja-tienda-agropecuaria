using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioSucursal
    {
        Task<bool> DeleteSucursal(int id);
        Task<IEnumerable<Sucursal>> Get();
        Task<Sucursal> GetById(int id);
        Task<bool> NewSucursal(Sucursal sucursal);
        Task<bool> UpdateSucursal(int id, Sucursal sucursal);
    }
}
