using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs
{
    public class RespuestaConstruirTokenRecuperacionDTO
    {
        public required string EmailDestino { get; set; }
        public required string Asunto { get; set; }
        public required string Cuerpo { get; set; }
    }
}
