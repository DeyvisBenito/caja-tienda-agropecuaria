using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.AuthUseCases.AuthCommands
{
    public class LoginConGoogle
    {
        private readonly IRepositorioAuth repositorioAuth;

        public LoginConGoogle(IRepositorioAuth repositorioAuth)
        {
            this.repositorioAuth = repositorioAuth;
        }

        // Caso de uso Login con Google
        public async Task<bool> ExecuteAsync(string credenciales)
        {

            var respuesta = await repositorioAuth.LoginConGoogle(credenciales);

            return respuesta;
        }
    }
}
