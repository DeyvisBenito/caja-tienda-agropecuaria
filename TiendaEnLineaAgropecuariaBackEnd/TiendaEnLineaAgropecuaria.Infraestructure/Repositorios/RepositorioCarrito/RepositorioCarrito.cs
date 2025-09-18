using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCarrito
{
    public class RepositorioCarrito: IRepositorioCarrito
    {
        private readonly ApplicationDBContext dBContext;
        private readonly UserManager<ApplicationUser> userManager;

        public RepositorioCarrito(ApplicationDBContext dBContext, UserManager<ApplicationUser> userManager)
        {
            this.dBContext = dBContext;
            this.userManager = userManager;
        }

        public async Task<Carrito> GetCarritoByUserId(string userId)
        {
            var userDB = await userManager.FindByIdAsync(userId);
            if (userDB is null)
            {
                throw new KeyNotFoundException("Usuario no logueado");
            }

            var carritoDB = await dBContext.Carritos.FirstOrDefaultAsync(x => x.IdUser == userId);
            if (carritoDB is null)
            {
                carritoDB = new Carrito
                {
                    IdUser = userId,
                    Fecha = DateTime.UtcNow
                };
                dBContext.Carritos.Add(carritoDB);
                await dBContext.SaveChangesAsync();
            }



            return carritoDB;
        }
    }
}
