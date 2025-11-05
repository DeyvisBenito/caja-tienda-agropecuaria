using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioClientes
    {
        Task<bool> Delete(int id);
        Task<Cliente> Get(int id);
        Task<IEnumerable<Cliente>> Get();
        Task<Cliente> GetByVentaId(int id);
        Task<bool> NewCliente(Cliente cliente);
        Task<bool> Update(int id, Cliente cliente);
    }
}
