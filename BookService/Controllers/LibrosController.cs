using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookSvc.Models;

namespace BookSvc.Controllers
{
    public class LibrosController : ApiController
    {
        private BookServiceContext db = new BookServiceContext();

        /// <summary>
        /// obtener listado de libros con GET
        /// </summary>
        /// <returns></returns>
        public IQueryable<LibroDTO> GetBooks()
        {
            var books = from l in db.Libros
                        select new LibroDTO()
                        {
                            Id = l.Id,
                            Titulo = l.Titulo,
                            Autor = l.Autor.Nombre
                        };
            return books;
        }

        /// <summary>
        /// obtener un libro con GET
        /// </summary>
        /// <param name="id">ID del libro</param>
        /// <returns></returns>
        [ResponseType(typeof(LibroDetalleDTO))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var book = await db.Libros.Include(l => l.Autor).Select(l => new LibroDetalleDTO()
                            {
                                Id = l.Id,
                                Titulo = l.Titulo,
                                Anio = l.Anio,
                                Precio = l.Precio,
                                Autor = l.Autor.Nombre,
                                Genero = l.Genero
                            }).SingleOrDefaultAsync(l => l.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        /// <summary>
        /// modificar un libro con PUT
        /// </summary>
        /// <param name="id">ID del libro</param>
        /// <param name="updBook">Objeto libro</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook(int id, Libro updBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updBook.Id)
            {
                return BadRequest();
            }

            db.Entry(updBook).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// crear un libro con POST
        /// </summary>
        /// <param name="newBook">libro a crear</param>
        /// <returns></returns>
        [ResponseType(typeof(Libro))]
        public async Task<IHttpActionResult> PostBook(Libro newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Libros.Add(newBook);
            await db.SaveChangesAsync();

            db.Entry(newBook).Reference(l => l.Autor).Load();
            var dto = new LibroDTO()
            {
                Id = newBook.Id,
                Titulo = newBook.Titulo,
                Autor = newBook.Autor.Nombre
            };

            return CreatedAtRoute("DefaultApi", new { id = newBook.Id }, dto);
        }

        /// <summary>
        /// eliminar un libro
        /// </summary>
        /// <param name="id">ID del libro a eliminar</param>
        /// <returns></returns>
        [ResponseType(typeof(Libro))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            Libro book = await db.Libros.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Libros.Remove(book);
            await db.SaveChangesAsync();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Libros.Count(e => e.Id == id) > 0;
        }
    }
}