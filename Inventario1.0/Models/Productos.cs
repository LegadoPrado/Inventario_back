using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventario1._0.Models
{
    public class Productos
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string sku { get; set; }
        public int cantidad { get; set; }
        public int sucursalId { get; set; }
        public virtual Sucursales Sucursal { get; set; }
    }
}
