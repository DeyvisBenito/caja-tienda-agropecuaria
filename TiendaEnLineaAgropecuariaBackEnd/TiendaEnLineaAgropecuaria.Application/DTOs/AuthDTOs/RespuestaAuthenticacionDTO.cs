using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs
{
    public class RespuestaAuthenticacionDTO
    {
        public required string Token { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public List<string> Errores { get; set; } = [];
        public bool EsExitoso { get; set; }
    }
}
