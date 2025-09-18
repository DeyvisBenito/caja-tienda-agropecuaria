using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task Borrar(string urlFoto, string contenedor);
        Task<string> Almacenar(string contenedor, IFormFile foto);
        async Task<string> Editar(string urlFoto, string contenedor, IFormFile foto)
        {
            await Borrar(urlFoto, contenedor);
            return await Almacenar(contenedor, foto);
        }
    }
}
