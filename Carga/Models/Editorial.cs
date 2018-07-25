using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class Editorial
    {
        public int ID { get; set; }
        public int InfAdicionalID { get; set; }
        public int DireccionID { get; set; }
        public string Nombre { get; set; }
    }
}