using QuestionApp.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace QuestionApp.Controllers
{
    public class logsController : Controller
    {
        private DBContext db = new DBContext();
        // GET: logs
        public ActionResult Index()
        {
            return View(db.Log.ToList());
        }
        public ActionResult ErrorIndex()
        {
            return View(db.Error.ToList());
        }
        public ActionResult ErrorDetails(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogError error = db.Error.Find(id);
            if (error == null)
            {
                return HttpNotFound();
            }
            return View(error);
        }
    }
}