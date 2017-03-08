using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BookSvc.Models
{
    public class BookServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BookServiceContext() : base("name=BookServiceContext")
        {
            // para obtener los strings que genera EF
            Database.Log = res => System.Diagnostics.Debug.WriteLine(res);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<BookSvc.Models.Autor> Autores { get; set; }

        public System.Data.Entity.DbSet<BookSvc.Models.Libro> Libros { get; set; }
    }
}
