using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventario1._0.Models
{
    public class Usuarios
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string passwd { get; set; }
        public virtual Models.Bitacora bitacora { get; set; }
    }
}
