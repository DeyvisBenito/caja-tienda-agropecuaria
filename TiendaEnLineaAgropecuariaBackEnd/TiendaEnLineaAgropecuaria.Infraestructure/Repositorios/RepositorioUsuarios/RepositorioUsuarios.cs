using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgrepecuaria.Domain.ValueObjects;
using TiendaEnLineaAgropecuaria.Application.DTOs.AuthDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.UsuariosDTOs;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioUsuarios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDBContext dbContext;

        public RepositorioUsuarios(UserManager<ApplicationUser> userManager, ApplicationDBContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<object>> Get()
        {
            var usuarios = await userManager.Users
                .Include(u => u.Sucursal).ThenInclude(x => x!.Estado)
                .Include(u => u.Estado)
                .ToListAsync();

            var resultado = new List<object>();

            foreach (var usuario in usuarios)
            {
                var estado = usuario.Estado?.Nombre;
                var claims = await userManager.GetClaimsAsync(usuario);
                if (usuario.Sucursal is not null)
                {
                    var sucursal = new SucursalDTO
                    {
                        Id = usuario.Sucursal.Id,
                        Nombre = usuario.Sucursal.Nombre,
                        Ubicacion = usuario.Sucursal.Ubicacion,
                        EstadoId = usuario.Sucursal.EstadoId,
                        Estado = usuario.Sucursal.Estado!.Nombre
                    };

                    resultado.Add(new
                    {
                        usuario.Id,
                        usuario.UserName,
                        usuario.Email,
                        usuario.PerfilCompletado,
                        usuario.RecibirNotificaciones,
                        usuario.EstadoId,
                        estado,
                        usuario.SucursalId,
                        sucursal,
                        Claims = claims.Select(c => new { c.Type, c.Value })
                    });
                }
                else
                {
                    resultado.Add(new
                    {
                        usuario.Id,
                        usuario.UserName,
                        usuario.Email,
                        usuario.PerfilCompletado,
                        usuario.RecibirNotificaciones,
                        usuario.EstadoId,
                        estado,
                        usuario.SucursalId,
                        usuario.Sucursal,
                        Claims = claims.Select(c => new { c.Type, c.Value })
                    });
                }
            }

            return resultado;
        }

        public async Task<object> GetById(string id)
        {
            var usuario = await userManager.Users
                .Include(u => u.Sucursal).ThenInclude(x => x!.Estado)
                .Include(u => u.Estado)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (usuario is null)
            {
                throw new KeyNotFoundException("El usuario buscado no existe");
            }

            var resultado = new object();

            var estado = usuario.Estado?.Nombre;
            var claims = await userManager.GetClaimsAsync(usuario);

            if (usuario.Sucursal is not null)
            {
                var sucursal = new SucursalDTO
                {
                    Id = usuario.Sucursal.Id,
                    Nombre = usuario.Sucursal.Nombre,
                    Ubicacion = usuario.Sucursal.Ubicacion,
                    EstadoId = usuario.Sucursal.EstadoId,
                    Estado = usuario.Sucursal.Estado!.Nombre
                };

                resultado = new
                {
                    usuario.Id,
                    usuario.UserName,
                    usuario.Email,
                    usuario.PerfilCompletado,
                    usuario.RecibirNotificaciones,
                    usuario.EstadoId,
                    estado,
                    usuario.SucursalId,
                    sucursal,
                    Claims = claims.Select(c => new { c.Type, c.Value })
                };
            }
            else
            {
                resultado = new
                {
                    usuario.Id,
                    usuario.UserName,
                    usuario.Email,
                    usuario.PerfilCompletado,
                    usuario.RecibirNotificaciones,
                    usuario.EstadoId,
                    estado,
                    usuario.SucursalId,
                    usuario.Sucursal,
                    Claims = claims.Select(c => new { c.Type, c.Value })
                };
            }

            return resultado;
        }

        // Solo verifica si el email ya existe o no y si esta eliminado
        public async Task<bool> GetByEmailBool(string email)
        {
            var usuario = await userManager.FindByEmailAsync(email);
            if(usuario is null || usuario.EstadoId != (int)EstadosEnum.Eliminado)
            {
                return false;
            }
            return true;
        }

        // Registrar Usuarios con Email
        public async Task<ResultadoRegistroUsuario> RegistrarUsuarioConEmail(Usuario usuario)
        {
            var appUsuario = new ApplicationUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                RecibirNotificaciones = usuario.RecibirNotificaciones,
                PerfilCompletado = true,
                SucursalId = usuario.SucursalId,
                EstadoId = (int)EstadosEnum.Activo
            };
            var userExistElim = await userManager.FindByEmailAsync(appUsuario.Email);
            if (userExistElim is not null)
            {
                if (userExistElim.EstadoId == (int)EstadosEnum.Eliminado)
                {
                    userExistElim.EstadoId = (int)EstadosEnum.Activo;
                    userExistElim.SucursalId = usuario.SucursalId;

                    // Cambiar a la nueva password
                    var token = await userManager.GeneratePasswordResetTokenAsync(userExistElim);
                    var resultadoCambio = await userManager.ResetPasswordAsync(userExistElim, token, usuario.Password!);
                    if (!resultadoCambio.Succeeded)
                    {
                        var resultadoError = new ResultadoRegistroUsuario
                        {
                            EsExitoso = false
                        };

                        foreach (var error in resultadoCambio.Errors)
                        {
                            resultadoError.Errores.Add(error.Description);
                        }
                        return resultadoError;
                    }

                    // Guardar cambios
                    await userManager.UpdateAsync(userExistElim);

                    // Devolvemos éxito y salimos
                    return new ResultadoRegistroUsuario
                    {
                        EsExitoso = true,
                        IdUsuario = userExistElim.Id,
                        SucursalId = userExistElim.SucursalId
                    };
                }

            }
            var resultado = await userManager.CreateAsync(appUsuario, usuario.Password!);

            if (resultado.Succeeded)
            {
                var claim = new Claim("rol", "vendedor");
                await userManager.AddClaimAsync(appUsuario, claim);
                return new ResultadoRegistroUsuario
                {
                    EsExitoso = true,
                    IdUsuario = appUsuario.Id,
                    SucursalId = appUsuario.SucursalId
                };
            }
            else
            {
                var resultadoError = new ResultadoRegistroUsuario
                {
                    EsExitoso = false
                };

                foreach (var error in resultado.Errors)
                {
                    resultadoError.Errores.Add(error.Description);
                }

                return resultadoError;
            }
        }

        public async Task<bool> UpdateUsuario(string id, UsuarioUpdateDTO usuarioUpdateDTO)
        {
            var usuarioAActualizar = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (usuarioAActualizar is null)
            {
                throw new KeyNotFoundException("El usuario a actualizar no existe");
            }
            var sucursalExist = await dbContext.Sucursales.AnyAsync(x => x.Id == usuarioUpdateDTO.SucursalId);
            if (!sucursalExist)
            {
                throw new KeyNotFoundException("La sucursal a colocar no existe");
            }
            var estadoExist = await dbContext.Estados.AnyAsync(x => x.Id == usuarioUpdateDTO.EstadoId);
            if (!estadoExist)
            {
                throw new KeyNotFoundException("El estado a colocar no existe");
            }

            usuarioAActualizar.Email = usuarioUpdateDTO.Email;
            usuarioAActualizar.SucursalId = usuarioUpdateDTO.SucursalId;
            usuarioAActualizar.EstadoId = usuarioUpdateDTO.EstadoId;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var usuario = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario is null || usuario.EstadoId == (int)EstadosEnum.Eliminado)
            {
                throw new KeyNotFoundException("El usuario a eliminar no existe");
            }

            usuario.EstadoId = (int)EstadosEnum.Eliminado;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
