using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Carga.Models;
using System.Data.OleDb;
using System.IO;

namespace Carga.Controllers
{
    public class PersonaController : Controller
    {
        private LibreriaEntities db = new LibreriaEntities();

        // GET: Persona
        public ActionResult Index()
        {
            return View(db.Persona.Where(c=>c.ID<150).ToList());
        }

        // GET: Persona/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Persona/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persona/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InfAdicionalID,Nombre,ApellidoM,ApellidoP,FechaNacimiento,CURP,RFC")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Persona.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(persona);
        }

        // GET: Persona/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Persona/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InfAdicionalID,Nombre,ApellidoM,ApellidoP,FechaNacimiento,CURP,RFC")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        // GET: Persona/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Almacenamiento macivo
        //[HttpPost]
        public ActionResult Carga()
        {
            try
            {
                string fileLocation = Server.MapPath("/Content/PLD_ FT actualizadas a 28 de junio de 2018.xlsx");
                string fileExtension = Path.GetExtension(fileLocation);

                string excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                //Avaluacion del tipo de extención del documento.
                if (fileExtension == ".xls")
                {
                    //Se asigna la cadena de conexion para la extencion xls
                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                //Avaluacion del tipo de extención del documento.
                else if (fileExtension == ".xlsx")
                {
                    //Se asigna la cadena de conexion para la extencion xlsx
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                excelConnection.Open();
                DataTable dt = new DataTable();

                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                String[] excelSheets = new String[dt.Rows.Count];
                int t = 0;
                foreach (DataRow row in dt.Rows)
                {
                    //Se obtiene el nombre de cada una de las hojas/tablas
                    excelSheets[t] = row["TABLE_NAME"].ToString();
                    t++;
                }
                excelConnection.Close();
                string HojaASubir = "";
                foreach(string Nombre in excelSheets)
                {
                    if(Nombre.Contains("PEPs candidatos")) {
                        HojaASubir = Nombre;
                    }
                }

                DataSet Hoja = GenerarDataset(excelConnectionString, HojaASubir);
                foreach (DataTable Tabla in Hoja.Tables)
                {
                    foreach (DataRow Fila in Tabla.Rows)
                    {
                        Persona per = new Persona();
                        for (int n = 0; n < Tabla.Columns.Count; n++)
                        {
                            switch (Tabla.Columns[n].ColumnName)
                            {
                                case "NOMBRE (S)":
                                    per.Nombre = Fila[Tabla.Columns[n].ColumnName].ToString();
                                    break;
                                case "APELLIDO PATERNO":
                                        per.ApellidoP = Fila[Tabla.Columns[n].ColumnName].ToString();
                                    break;
                                case "APELLIDO MATERNO":
                                    per.ApellidoM = Fila[Tabla.Columns[n].ColumnName].ToString();
                                    break;
                            }
                        }
                        DateTime FechaActual = DateTime.Today;

                        per.InfAdicionalID = 1;
                        per.CURP = "Publica";
                        per.FechaNacimiento = FechaActual;
                        per.RFC = "Publica";
                        db.Persona.Add(per);
                        db.SaveChanges();
                    }
                }

            }
            catch(Exception ex)
            {
                string Error = ex.Message;
            }
            return View("Index");
        }
        private DataSet GenerarDataset(string ExcelConexion, string NameTablaExcel)
        {
            DataSet ds = new DataSet();

            string excelConnectionString = ExcelConexion;

            //Create Connection to Excel work book and add oledb namespace
            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
            excelConnection.Open();
            DataTable dt = new DataTable();
            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt == null)
            {
                return null;
            }

            string excelSheets = "";
            int t = 0;
            foreach (DataRow row in dt.Rows)
            {
                //Se obtiene el nombre de cada una de las hojas/tablas
                string NameTable = row["TABLE_NAME"].ToString();
                t++;
                //string Caracter = NameTable.Replace(@"'", "").Replace("$", ""); //NameTable.Substring(0, 1) + NameTable.Substring(NameTable.Length - 1, 1);
                if (NameTable.Contains(NameTablaExcel))
                {
                    excelSheets = NameTable;
                    break;
                }
            }
            //excelConnection.Close();

            //OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
            //string query = string.Format("Select * from [{0}]", excelSheets);
            string query = "select [NOMBRE (S)],[APELLIDO PATERNO],[APELLIDO MATERNO] from [" + excelSheets + "]";
            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection))
            {
                dataAdapter.Fill(ds);
            }
            excelConnection.Close();
            //*****************************************
            //ds.Tables[0].TableName = NameTablaExcel;
            ds.Tables[0].TableName = excelSheets;
            //*****************************************
            return ds;
        }
        #endregion

        public ActionResult Muestra()
        {
            ProcedureExec exec = new ProcedureExec();
            try {
                var tem = exec.Consulta("May");
                var dos = exec.ObtenerNombres(2);
            } catch(Exception ex) {
                var Exs = ex.Message;
            }


            return View("Index");
        }
    }
}
