﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs
{
    public class CredencialesUsuarioDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
