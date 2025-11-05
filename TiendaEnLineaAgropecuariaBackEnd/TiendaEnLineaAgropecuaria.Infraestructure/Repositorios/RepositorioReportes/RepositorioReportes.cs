using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioReportes
{
    public class RepositorioReportes : IRepositorioReportes
    {
        private readonly ApplicationDBContext dbContex;

        public RepositorioReportes(ApplicationDBContext dbContex)
        {
            this.dbContex = dbContex;
        }

        public async Task<IEnumerable<object>> BestClientesPorVentas()
        {
            var clientes = await dbContex.Clientes.Select(c => new
            {
                c.Id,
                c.Nit,
                c.Nombres,
                c.Apellidos,
                c.Email,
                c.Telefono,
                CantidadVentas = c.Ventas.Count(),
                TotalGastado = c.Ventas.Sum(v => v.Total)
            }).OrderByDescending(c => c.CantidadVentas).ToListAsync();

            return clientes;
        }

        public async Task<IEnumerable<object>> BestProveedores()
        {
            var proveedores = await dbContex.Proveedores.Select(p => new
            {
                p.Id,
                p.Nit,
                p.Nombres,
                p.Apellidos,
                p.Telefono,
                CantidadCompras = p.Compras.Count()
            }).OrderByDescending(c => c.CantidadCompras).ToListAsync();

            return proveedores;
        }

        public async Task<IEnumerable<object>> VentasDelDia()
        {
            var hoyUtc = DateTime.UtcNow.Date;
            var mañanaUtc = hoyUtc.AddDays(1);

            var ventas = await dbContex.Ventas.Include(x => x.Cliente).Include(x => x.Sucursal)
                .Where(v => v.FechaCreacion >= hoyUtc && v.FechaCreacion < mañanaUtc)
                .Select(v => new
                {
                    v.Id,
                    v.ClienteId,
                    NitCliente = v.Cliente!.Nit,
                    Cliente = v.Cliente!.Nombres + " " + v.Cliente.Apellidos,
                    Sucursal = v.Sucursal!.Nombre,
                    v.Total,
                    v.FechaCreacion,
                })
                .OrderByDescending(v => v.FechaCreacion)
                .ToListAsync();

            return ventas;
        }

        public async Task<IEnumerable<object>> ComprasDelDia()
        {
            var hoyUtc = DateTime.UtcNow.Date;
            var mañanaUtc = hoyUtc.AddDays(1);

            var compras = await dbContex.Compras.Include(x => x.Proveedor).Include(x => x.Sucursal)
                .Where(c => c.FechaCreacion >= hoyUtc && c.FechaCreacion < mañanaUtc)
                .Select(c => new
                {
                    c.Id,
                    c.ProveedorId,
                    NitProveedor = c.Proveedor!.Nit,
                    Proveedor = c.Proveedor!.Nombres + " " + c.Proveedor.Apellidos,
                    Sucursal = c.Sucursal!.Nombre,
                    c.Total,
                    c.FechaCreacion,
                })
                .OrderByDescending(c => c.FechaCreacion)
                .ToListAsync();

            return compras;
        }
    }
}
