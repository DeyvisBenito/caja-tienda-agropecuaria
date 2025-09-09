using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgrepecuaria.Domain.ValueObjects;
using TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.UsuariosDTOs;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioUsuarios
{
    public class RepositorioUsuarios: IRepositorioUsuarios
    {
        private readonly UserManager<ApplicationUser> userManager;

        public RepositorioUsuarios(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // Registrar Usuarios con Email
        public async Task<ResultadoRegistroUsuario> RegistrarUsuarioConEmail(Usuario usuario)
        {
            var appUsuario = new ApplicationUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                RecibirNotificaciones = usuario.RecibirNotificaciones,
                PerfilCompletado = true
            };

            var resultado = await userManager.CreateAsync(appUsuario, usuario.Password!);

            if (resultado.Succeeded)
            {
                return new ResultadoRegistroUsuario 
                {
                    EsExitoso = true
                };
            }
            else
            {
                var resultadoError = new ResultadoRegistroUsuario
                {
                    EsExitoso = false
                };

                foreach(var error in resultado.Errors)
                {
                    resultadoError.Errores.Add(error.Description);
                }

                return resultadoError;
            }
        }
    }
}
