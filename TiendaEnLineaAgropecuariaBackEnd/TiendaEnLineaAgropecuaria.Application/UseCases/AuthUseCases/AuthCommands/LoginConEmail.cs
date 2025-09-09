using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.AuthUseCases.AuthCommands
{
    public class LoginConEmail
    {
        private readonly IRepositorioAuth repositorioAuth;

        public LoginConEmail(IRepositorioAuth repositorioAuth)
        {
            this.repositorioAuth = repositorioAuth;
        }

        // Caso de uso Login con Email
        public async Task<bool> Execute(CredencialesUsuarioDTO credencialesUsuarioDTO)
        {
            var usuario = new Usuario
            {
                Email = credencialesUsuarioDTO.Email,
                Password = credencialesUsuarioDTO.Password
            };

            var respuesta = await repositorioAuth.LoginConEmail(usuario);

            return respuesta;
        }
    }
}
