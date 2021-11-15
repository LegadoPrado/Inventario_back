using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventario1._0.Models
{
    public class Bitacora
    {
        public int id { get; set; }
        public int idUser { get; set; }
        public string movimiento { get; set; }
        public DateTime fecha { get; set; }
        public virtual ICollection<Usuarios> usuarios { get; set; }
    }
}
