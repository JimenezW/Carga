using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class Localidad
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string TipoLocalidad { get; set; }
        public int CodigoPostal { get; set; }
        public string Zona { get; set; }
        public int MunicipioID { get; set; }
    }
}