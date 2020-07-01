using backend.Data;
using backend.HelperClasses;
using backend.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

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
            return db.Cursusinstanties.Include(c => c.Cursus).OrderBy(c => c.Startdatum);
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

        [HttpPost]
        public HttpResponseMessage PostCursusinstanties()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Er ging iets mis!");
            }

            var file = httpRequest.Files[0];
            var fileHelper = new FileHelper(db);

            var validFileMessage = fileHelper.FileIsValid(file);
            if (!validFileMessage.Equals("Ok"))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, validFileMessage);
            }

            fileHelper.AddCursussenFromFileToDatabase(file);

            return Request.CreateResponse(HttpStatusCode.Created, fileHelper.ReturnMessage());
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

        //private bool CursusinstantieExists(int id)
        //{
        //    return db.Cursusinstanties.Count(e => e.Id == id) > 0;
        //}
    }
}