using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventario1._0.Models
{
    public class Sucursales
    {
        public int id { get; set; }
        public string nombre { set; get; }
        public string direccion { set; get; }
        public string telefono { set; get; }
        public virtual ICollection<Productos> productos { get; set; }
    }
}
