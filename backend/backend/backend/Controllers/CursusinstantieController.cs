using backend.Data;
using backend.HelperClasses;
using backend.Models;
using System.Data.Entity;
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

        // GET: api/Cursusinstantie\?weeknummer=27&jaar=2020
        public IQueryable<Cursusinstantie> GetCursusinstanties(int weeknummer, int jaar)
        {
            return db.Cursusinstanties.Include(c => c.Cursus).ToList()
                .Where(c => c.Startdatum.GetWeekOfYear() == weeknummer && c.Startdatum.Year == jaar)
                .OrderBy(c => c.Startdatum)
                .AsQueryable();
        }

        [HttpPost]
        public HttpResponseMessage PostCursusinstanties()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, resources.BadRequestErrorMessage);
            }

            var fileStream = httpRequest.Files[0].InputStream;

            var fileHelper = new FileHelper(db);

            var validFileMessage = fileStream.IsValidFile();
            if (!validFileMessage.Equals("Valid"))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, validFileMessage);
            }

            fileHelper.AddCursussenFromFileToDatabase();

            return Request.CreateResponse(HttpStatusCode.Created, fileHelper.ReturnMessage());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}