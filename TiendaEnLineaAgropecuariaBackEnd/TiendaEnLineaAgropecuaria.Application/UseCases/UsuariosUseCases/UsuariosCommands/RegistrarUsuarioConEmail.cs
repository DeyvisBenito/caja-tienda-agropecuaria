using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.UsuariosDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.UsuariosUseCases.UsuariosCommands
{
    public class RegistrarUsuarioConEmail
    {
        private readonly IRepositorioUsuarios repositorioUsuarios;

        public RegistrarUsuarioConEmail(IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
        }

        // Registrar cliente con Email Caso de uso
        public async Task<RespuestaRegistroDTO> Execute(CredencialesRegistrarConEmailDTO credencialesRegistrarConEmailDTO)
        {

            var usuario = new Usuario
            {
                Email = credencialesRegistrarConEmailDTO.Email,
                RecibirNotificaciones = true,
                Password = credencialesRegistrarConEmailDTO.Password,
                SucursalId = credencialesRegistrarConEmailDTO.SucursalId
            };

            var respuesta = await repositorioUsuarios.RegistrarUsuarioConEmail(usuario);

            var resultadoRegistro = new RespuestaRegistroDTO
            {
                EsExitoso = respuesta.EsExitoso,
                Errores = respuesta.Errores,
                IdUsuario = respuesta.IdUsuario,
                SucursalId = respuesta.SucursalId
            };

            return resultadoRegistro;
        }
    }
}
