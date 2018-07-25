using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class Libro
    {
        public int ID { get; set; }
        public int AutorID { get; set; }
        public int CategoriaID { get; set; }
        public int EditorialID{ get; set; }
        public string Nombre { get; set; }
        public int? NPagina { get; set; }
        public string ISBN { get; set; }
        public string Sipnosis { get; set; }
        public string Foto { get; set; }
        public bool Eliminado { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaEliminado { get; set; }
    }
}