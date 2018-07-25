using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class Almacen
    {
        public int ID { get; set; }
        public int LibroID { get; set; }
        public int cantidad { get; set; }
        public bool Disponible { get; set; }
        public bool Eliminado { get; set; }
        public int DirecionID { get; set; }
    }
}