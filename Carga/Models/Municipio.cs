using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class Municipio
    {
        public int ID { get; set; }
        public int EstadoID { get; set; }
        public string Nombre { get; set; }
        public string NomAbreviado { get; set; }
        public int? ClaveMunicipio { get; set; }
    }
}