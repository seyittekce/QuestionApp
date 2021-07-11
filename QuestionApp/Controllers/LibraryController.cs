using QuestionApp.Models;
using QuestionApp.Models.Addon;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace QuestionApp.Controllers
{
    public class LibraryController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Library
        public ActionResult Index()
        {
            return View(db.QuizLibrary.ToList());
        }
        // GET: Library/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizLibrary quizLibrary = db.QuizLibrary.Find(id);
            if (quizLibrary == null)
            {
                return HttpNotFound();
            }
            return View(quizLibrary);
        }
        // GET: Library/Create
        public ActionResult Create()
        {
            ViewBag.sorular = db.Question.ToList();
            return View();
        }
        // POST: Library/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Questions,MailSend,StartDate,EndDate,CreatedDate,Comment")] QuizLibrary quizLibrary, string[] Question)
        {
            if (ModelState.IsValid)
            {
                quizLibrary.Questions = string.Join("|", Question);
                quizLibrary.CreatedDate = DateTime.Now.ToString();
                db.QuizLibrary.Add(quizLibrary);
                db.SaveChanges();
                Logger.LogKeeper(quizLibrary.Name + " Kütüphane oluşturdu");
                return RedirectToAction("Index");
            }
            return View(quizLibrary);
        }
        // GET: Library/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizLibrary quizLibrary = db.QuizLibrary.Find(id);
            quizLibrary.Question = quizLibrary.Questions.Split('|').ToArray();
            ViewBag.sorular = db.Question.ToList();
            if (quizLibrary == null)
            {
                return HttpNotFound();
            }
            return View(quizLibrary);
        }
        // POST: Library/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Questions,MailSend,StartDate,EndDate,CreatedDate,Comment")] QuizLibrary quizLibrary, string[] Question)
        {
            if (ModelState.IsValid)
            {
                quizLibrary.Questions = string.Join("|", Question);
                db.Entry(quizLibrary).State = EntityState.Modified;
                db.SaveChanges();
                Logger.LogKeeper(quizLibrary.Name + " adlı Kütüphaneyi Güncelledi");
                return RedirectToAction("Index");
            }
            return View(quizLibrary);
        }
        // GET: Library/Delete/5
        // POST: Library/Delete/5
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizLibrary quizLibrary = db.QuizLibrary.Find(id);
            db.QuizLibrary.Remove(quizLibrary);
            db.SaveChanges();
            return RedirectToAction("Index");
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
