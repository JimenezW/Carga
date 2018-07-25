using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class Autor:Persona
    {
        public int ID { get; set; }
        public int  PersonaID{ get; set; }
        public string Resena { get; set; }
        public string Foto { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaEliminado { get; set; }
        public bool Eliminado { get; set; }
    }
}