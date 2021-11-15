

using Inventario1._0.DbContexts;
using Inventario1._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventario1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("ObtenerProductos")]
        public async Task<ActionResult<List<Productos>>> Get()
        {
            List<Productos> productos = new List<Productos>();
            try
            {
                productos = await _context.Producto.ToListAsync();
                return productos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("ObtenerProducto/{id}")]
        public async Task<ActionResult<Productos>> GetById(int id, [FromHeader] string token)
        {
            Productos producto = new Productos();
            try
            {
                producto = await _context.Producto.Where(x => x.id == id).FirstOrDefaultAsync();
                if (producto == null)
                {
                    return BadRequest("Registro no encontrado");
                }
                return producto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("addProducto")]
        public async Task<ActionResult> Add([FromBody] Productos producto, [FromHeader] int idUser)
        {
            try
            {
                await _context.Producto.AddAsync(producto);
                await _context.SaveChangesAsync();
                await new Helpers.Bitacora(_context).Insert(idUser);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("updateProducto/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Productos producto, [FromHeader] int idUser)
        {
            try
            {
                producto.id = id;
                _context.Update(producto);
                await _context.SaveChangesAsync();
                await new Helpers.Bitacora(_context).Update(idUser);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("deleteProducto/{id}")]
        public async Task<ActionResult> Delete(int id, [FromHeader] int idUser)
        {
            try
            {                
                _context.Remove(new Productos() { id = id});
                await _context.SaveChangesAsync();
                await new Helpers.Bitacora(_context).Delete(idUser);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<ActionResult<bool>> Exist(int id)
        {
            var existe = await _context.Producto.AnyAsync(x => x.id == id);
            if (!existe)
            {
                return NotFound();
            }
            return true;
        }
    }
}
