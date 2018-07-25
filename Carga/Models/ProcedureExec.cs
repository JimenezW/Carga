using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Carga.Models
{
    public class ProcedureExec
    {
        LibreriaEntities db = new LibreriaEntities();

        public List<Persona> Consulta(string Apellido)
        {
            //List<Persona> per = new List<Persona>();

            var lista = db.Database.SqlQuery<Persona>("BuscarPorApellido @ApellidoM", new SqlParameter("@ApellidoM", Apellido)).ToList();
            //per = lista;
            /*foreach(Persona tem in lista)
            {
                per.Add(tem);
            }*/
            return lista;
        }

        public List<Object> ObtenerNombres(int PersonaID)
        {
            List<Object> tem = new List<object>();
            var listaNombres = db.Database.SqlQuery<Persona2>("ObtenerNombreCompleto @PersonaID", new SqlParameter("@PersonaID", 2)).ToList();
            foreach(Persona2 temp in listaNombres)
            {
                tem.Add(new { ID = temp.ID, NombreCompleto = temp.NombreCompleto });
            }
            return tem;
        }

        private class Persona2
        {
            public int ID { get; set; }
            public string NombreCompleto { get; set; }
        }
    }
}