using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class Persona
    {
        public int ID { get; set; }
        public int InfAdicionalID { get; set; }
        public string Nombre { get; set; }
        public string ApellidoM { get; set; }
        public string ApellidoP { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }

    }
}