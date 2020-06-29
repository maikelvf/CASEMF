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
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    public class CursusController : ApiController
    {
        private CursusDBContext db = new CursusDBContext();

        // GET: api/Cursus
        public IQueryable<Cursus> GetCursussen()
        {
            return db.Cursussen;
        }

        // GET: api/Cursus/5
        [ResponseType(typeof(Cursus))]
        public async Task<IHttpActionResult> GetCursus(int id)
        {
            Cursus cursus = await db.Cursussen.FindAsync(id);
            if (cursus == null)
            {
                return NotFound();
            }

            return Ok(cursus);
        }

        // PUT: api/Cursus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCursus(int id, Cursus cursus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cursus.Id)
            {
                return BadRequest();
            }

            db.Entry(cursus).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursusExists(id))
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

        // POST: api/Cursus
        [ResponseType(typeof(Cursus))]
        public async Task<IHttpActionResult> PostCursus(Cursus cursus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cursussen.Add(cursus);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cursus.Id }, cursus);
        }

        // DELETE: api/Cursus/5
        [ResponseType(typeof(Cursus))]
        public async Task<IHttpActionResult> DeleteCursus(int id)
        {
            Cursus cursus = await db.Cursussen.FindAsync(id);
            if (cursus == null)
            {
                return NotFound();
            }

            db.Cursussen.Remove(cursus);
            await db.SaveChangesAsync();

            return Ok(cursus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CursusExists(int id)
        {
            return db.Cursussen.Count(e => e.Id == id) > 0;
        }
    }
}