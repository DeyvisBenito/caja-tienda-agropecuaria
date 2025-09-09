using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioAuth
{
    public class RepositorioAuth : IRepositorioAuth
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly GenerarPayloadDeGoogle generarPayloadDeGoogle;

        public RepositorioAuth(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                IConfiguration configuration, GenerarPayloadDeGoogle generarPayloadDeGoogle)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.generarPayloadDeGoogle = generarPayloadDeGoogle;
        }


        // Repositorio Login con Email
        public async Task<bool> LoginConEmail(Usuario usuario)
        {
            var usuarioDB = await userManager.FindByEmailAsync(usuario.Email);
            if (usuarioDB is null)
            {
                throw new KeyNotFoundException("Login Incorrecto");
            }

            var resultado = await signInManager.CheckPasswordSignInAsync(usuarioDB,
                usuario.Password!, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Logueo con Google
        public async Task<bool> LoginConGoogle(string credenciales)
        {
            UserLoginInfo info;
            GoogleJsonWebSignature.Payload payload;

            try
            {
                var credencialesGoogle = new CredencialesGoogleDTO
                {
                    Credenciales = credenciales
                };

                payload = await generarPayloadDeGoogle.DesifrarPayloadGoogle(credencialesGoogle);

                // Informacion del login
                info = new UserLoginInfo("Google", payload.Subject, "Google");
            }
            catch (InvalidJwtException ex)
            {
                throw new UnauthorizedAccessException("Token de Google inválido");
            }

            // Si ya existe un usuario con este google
            var usuarioDBLogin = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (usuarioDBLogin is not null)
            {
                return true;
            }

            // Si no existe el correo con este google, verificamos si ya existe alguno registrado en la tabla
            var usuarioDB = await userManager.FindByEmailAsync(payload.Email);
            if (usuarioDB is not null)
            {
                // Si existe, lo asignamos con el login de Google
                var result = await userManager.AddLoginAsync(usuarioDB, info);
                if (!result.Succeeded)
                {
                    throw new Exception("No se pudieron vincular las cuentas.");
                }
                return true;
            }

            // Si no existe el correo, lo creamos y lo asignamos con el login de Google
            var nuevoUsuario = new ApplicationUser
            {
                UserName = payload.Email,
                Email = payload.Email,
                PerfilCompletado = false,
                EmailConfirmed = true // porque Google ya confirmó el correo
            };

            var resultado = await userManager.CreateAsync(nuevoUsuario);
            if (!resultado.Succeeded)
            {
                throw new Exception("No se pudo crear el usuario");
            }

            var resultadoAggregarLogin = await userManager.AddLoginAsync(nuevoUsuario, info);
            if (!resultadoAggregarLogin.Succeeded)
            {
                throw new Exception("No se pudo vincular Google con el nuevo usuario");
            }

            return true;


        }


       
    }
}
