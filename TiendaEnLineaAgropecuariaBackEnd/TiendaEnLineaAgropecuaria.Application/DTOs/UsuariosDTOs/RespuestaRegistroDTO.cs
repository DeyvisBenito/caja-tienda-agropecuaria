using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.UsuariosDTOs
{
    public class RespuestaRegistroDTO
    {
        public string? Token { get; set; }
        public string? IdUsuario { get; set; }
        public int? SucursalId { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public List<string> Errores { get; set; } = [];
        public bool EsExitoso { get; set; }
    }
}
