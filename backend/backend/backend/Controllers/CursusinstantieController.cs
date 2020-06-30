using backend.Data;
using backend.HelperClasses;
using backend.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend.Controllers
{
    public class CursusinstantieController : ApiController
    {
        private CursusDBContext db = new CursusDBContext();

        public CursusinstantieController()
        {
        }

        public CursusinstantieController(CursusDBContext context)
        {
            db = context;
        }

        // GET: api/Cursusinstantie
        public IQueryable<Cursusinstantie> GetCursusinstanties()
        {
            return db.Cursusinstanties.Include(c => c.Cursus);
        }

        //// GET: api/Cursusinstantie/5
        //[ResponseType(typeof(Cursusinstantie))]
        //public async Task<IHttpActionResult> GetCursusinstantie(int id)
        //{
        //    Cursusinstantie cursusinstantie = await db.Cursusinstanties.FindAsync(id);
        //    if (cursusinstantie == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(cursusinstantie);
        //}

        //// PUT: api/Cursusinstantie/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutCursusinstantie(int id, Cursusinstantie cursusinstantie)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != cursusinstantie.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(cursusinstantie).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CursusinstantieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        [Route("api/toevoegen")]
        [HttpPost]
        public HttpResponseMessage PostCursusinstanties()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Er ging iets mis!");
            }

            var file = httpRequest.Files[0];

            FileHelper.AddCursussenFromFileToDatabase(file, db);

            return Request.CreateResponse(HttpStatusCode.Created, FileHelper.ReturnMessage());
        }

        //// DELETE: api/Cursusinstantie/5
        //[ResponseType(typeof(Cursusinstantie))]
        //public async Task<IHttpActionResult> DeleteCursusinstantie(int id)
        //{
        //    Cursusinstantie cursusinstantie = await db.Cursusinstanties.FindAsync(id);
        //    if (cursusinstantie == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Cursusinstanties.Remove(cursusinstantie);
        //    await db.SaveChangesAsync();

        //    return Ok(cursusinstantie);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CursusinstantieExists(int id)
        {
            return db.Cursusinstanties.Count(e => e.Id == id) > 0;
        }
    }
}