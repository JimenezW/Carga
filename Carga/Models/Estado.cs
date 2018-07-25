using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class Estado
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string ClaveEstado { get; set; }
        public string NomAbreviado { get; set; }
    }
}