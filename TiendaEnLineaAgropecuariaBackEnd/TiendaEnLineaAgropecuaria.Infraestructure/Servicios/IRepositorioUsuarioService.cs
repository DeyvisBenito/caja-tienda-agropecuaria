using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.ValueObjects;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public interface IRepositorioUsuarioService
    {
        Task<IEnumerable<ApplicationUser>> Get();
        Task<ResultadoRegistroUsuario> RegistrarUsuarioConEmail(Usuario usuario);
    }
}
