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
    /// <summary>
    /// 
    /// </summary>
    public class AutoresController : ApiController
    {
        private BookServiceContext db = new BookServiceContext();

        /// <summary>
        /// obtener listado de autores con GET
        /// </summary>
        /// <returns></returns>
        public IQueryable<Autor> GetAuthors()
        {
            return db.Autores;
        }

        /// <summary>
        /// obtener un autor en particular de manera asíncrona con GET
        /// </summary>
        /// <param name="id">ID del autor</param>
        /// <returns></returns>
        [ResponseType(typeof(Autor))]
        public async Task<IHttpActionResult> GetAuthor(int id)
        {
            Autor objAuthor = await db.Autores.FindAsync(id);
            if (objAuthor == null)
            {
                return NotFound();
            }

            return Ok(objAuthor);
        }

        /// <summary>
        /// Modificar un autor de manera asíncrona con PUT
        /// </summary>
        /// <param name="id">ID del autor para modificar</param>
        /// <param name="updAuthor">Objeto autor modificado</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAuthor(int id, Autor updAuthor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updAuthor.Id)
            {
                return BadRequest();
            }

            db.Entry(updAuthor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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
        /// Crear un nuevo autor asíncrono con POST
        /// </summary>
        /// <param name="newAuthor">Objeto autor nuevo</param>
        /// <returns></returns>
        [ResponseType(typeof(Autor))]
        public async Task<IHttpActionResult> PostAuthor(Autor newAuthor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autores.Add(newAuthor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = newAuthor.Id }, newAuthor);
        }

        /// <summary>
        /// Eliminar un autor
        /// </summary>
        /// <param name="id">ID del autor a eliminar</param>
        /// <returns></returns>
        [ResponseType(typeof(Autor))]
        public async Task<IHttpActionResult> DeleteAuthor(int id)
        {
            Autor objAuthor = await db.Autores.FindAsync(id);
            if (objAuthor == null)
            {
                return NotFound();
            }

            db.Autores.Remove(objAuthor);
            await db.SaveChangesAsync();

            return Ok(objAuthor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorExists(int id)
        {
            return db.Autores.Count(e => e.Id == id) > 0;
        }
    }
}