using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class Direccion
    {
        public int ID { get; set; }
        public int LocalidadID { get; set; }
        public string Calle { get; set; }
        public bool DomicilioActual { get; set; }
        public bool Eliminado { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}