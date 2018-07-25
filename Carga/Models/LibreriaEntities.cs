using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Carga.Models
{
    public class LibreriaEntities:DbContext
    {
        public LibreriaEntities():base("Libreria")
        {

        }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Almacen> Almacen { get; set; }
        public  DbSet<Libro> Libro { get; set; }
        public  DbSet<Autor> Autor { get; set; }
        public  DbSet<InformacionAdicional> InformacionAdicional { get; set; }
        public  DbSet<Localidad> Localidad { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Editorial> Editorial { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}