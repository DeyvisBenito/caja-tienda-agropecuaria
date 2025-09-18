using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.InventariosDTO
{
    public class InventarioCreacionDTO
    {
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public int TipoProductoId { get; set; }
        [Required]
        public int EstadoId { get; set; }
        [Required]
        public int BodegaId { get; set; }
        [Required]
        public required string Marca { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public IFormFile? Foto { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
