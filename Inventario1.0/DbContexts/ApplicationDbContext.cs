using Inventario1._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventario1._0.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Sucursales> Sucursal { get; set; } //tabla sucursales
        public DbSet<Productos> Producto { get; set; } //tabla productos
        public DbSet<Usuarios> Usuarios { get; set; } //tabla usuarios
        public DbSet<Bitacora> Bitacoras { get; set; } //tabla bitacoras
    }
}
