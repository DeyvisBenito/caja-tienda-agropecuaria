using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.TipoProductosDTOs
{
    public class TipoProductosDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Estado { get; set; }
        public int EstadoId { get; set; }
        public required string Categoria { get; set; }
        public int CategoriaId { get; set; }
        public int TipoMedidaId { get; set; }
        public  required string TipoMedida { get; set; }
    }
}
