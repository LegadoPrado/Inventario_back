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
        public class SucursalController : ControllerBase
        {
            private readonly ApplicationDbContext _context;
            public SucursalController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet("ObtenerSucursales")]
            public async Task<ActionResult<List<Sucursales>>> Get()
            {
                List<Sucursales> sucursales = new List<Sucursales>();
                try
                {
                    sucursales = await _context.Sucursal.Include(x => x.productos).ToListAsync();
                    return sucursales;
                }
                catch (Exception)
                {

                    throw;
                }
            }

        [HttpGet("ObtenerProdBySucur")]
        public async Task<ActionResult<List<Productos>>> Get2()
        {
            List<Sucursales> sucursales = new List<Sucursales>();
            List<Productos> productos = new List<Productos>();

            try
            {
                sucursales = await _context.Sucursal.Include(x => x.productos).ToListAsync();

                foreach (var item in sucursales)
                {
                    foreach (var item2 in item.productos)
                    {
                        if(item2.cantidad != 0)
                        {
                            productos.Add(item2);
                        }
                    }
                }


                return productos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("ObtenerSucursal/{id}")]
            public async Task<ActionResult<Sucursales>> GetById(int id)
            {
                Sucursales sucursal = new Sucursales();
                try
                {
                    sucursal = await _context.Sucursal.Where(x => x.id == id).Include(x => x.productos).FirstOrDefaultAsync();
                    if (sucursal == null)
                    {
                        return BadRequest("Registro no encontrado");
                    }
                    return sucursal;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            [HttpPost("addSucursal")]
            public async Task<ActionResult> Add([FromBody] Sucursales sucursal, [FromHeader] int idUser)
            {
                try
                {
                    await _context.Sucursal.AddAsync(sucursal);
                    await _context.SaveChangesAsync();
                    await new Helpers.Bitacora(_context).Insert(idUser);
                    return Ok();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            [HttpPost("updateSucursal/{id}")]
            public async Task<ActionResult> Update(int id, [FromBody] Sucursales sucursal, [FromHeader] int idUser)
            {
                try
                {
                    sucursal.id = id;
                    _context.Update(sucursal);
                    await _context.SaveChangesAsync();
                await new Helpers.Bitacora(_context).Update(idUser);
                return Ok();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            [HttpPost("deleteSucursal/{id}")]
            public async Task<ActionResult> Delete(int id, [FromHeader] int idUser)
            {
                try
                {
                    _context.Remove(new Sucursales() { id = id });
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
                var existe = await _context.Sucursal.AnyAsync(x => x.id == id);
                if (!existe)
                {
                    return NotFound();
                }
                return true;
            }
        }
    }

