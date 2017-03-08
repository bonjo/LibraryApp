namespace BookSvc.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BookSvc.Models.BookServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookSvc.Models.BookServiceContext context)
        {
            context.Autores.AddOrUpdate(
              new Autor { Id = 1,Nombre="Autor Uno" },
              new Autor { Id=2,Nombre="Autor Dos"},
              new Autor { Id=3,Nombre="Autor Tres" }
            );
            context.Libros.AddOrUpdate(
                new Libro { Id = 1, Titulo = "Libro 1 Autor 1", AutorId = 1, Genero = "Terror", Anio = 1983, Precio = 12.38M });
        }
    }
}
