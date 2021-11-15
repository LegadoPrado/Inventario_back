using Inventario1._0.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventario1._0.Helpers
{
    public class Bitacora
    {
        private readonly ApplicationDbContext _context;
        public Bitacora(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<bool>> Insert(int idUser) => await addBitacora(idUser, "Inserta");

        public async Task<ActionResult<bool>> Update(int idUser) => await addBitacora(idUser, "Modifica");

        public async Task<ActionResult<bool>> Delete(int idUser) => await addBitacora(idUser, "Elimina");

        private async Task<ActionResult<bool>> addBitacora(int idUser, string action)
        {          
            await _context.Bitacoras.AddAsync(new Models.Bitacora
            {
                idUser = idUser,
                movimiento = action,
                fecha = DateTime.Parse(String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now))
            });
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
