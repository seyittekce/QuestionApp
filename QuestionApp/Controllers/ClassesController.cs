using Microsoft.Ajax.Utilities;
using QuestionApp.Models;
using QuestionApp.Models.Addon;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace QuestionApp.Controllers
{
    public class ClassesController : Controller
    {
        private DBContext db = new DBContext();
        [HttpGet]
        public ActionResult StudentResult(int LibraryID, int? ClassID)
        {
            TempData["class"] = ClassID;
            return View(db.Answers.Where(x => x.LibraryID == LibraryID && x.ClassID == ClassID && x.Status == "0").ToList());
        }
        // GET: Classes
        public ActionResult Index()
        {
            return View(db.Class.ToList());
        }
        // GET: Classes/Details/5
        public ActionResult Result(int? QuizID,int? StudentID)
        {
            var FindLib = db.QuizLibrary.Find(QuizID);
            TempData["QuizID"] = FindLib.ID;
            TempData["StudentID"] = StudentID;
            var FindStudent = db.Students.Find(StudentID);
            var FindResult = db.Answers.Where(x => x.LibraryID == FindLib.ID && x.StudentID == FindStudent.ID).ToList();
            return View(FindResult);
        
        }
        [HttpPost]
        public ActionResult Result(int? QuizID, int? StudentID,int Answer,string Dogru, int ss)
        {
            var FindLib = db.QuizLibrary.Find(QuizID);
            TempData["QuizID"] = FindLib.ID;
            TempData["StudentID"] = StudentID;
            var FindStudent = db.Students.Find(StudentID);
            var FindResult = db.Answers.Where(x => x.LibraryID == FindLib.ID && x.StudentID == FindStudent.ID).ToList();
            if (Dogru!=null)
            {
                var ExamResultChange = db.Answers.Find(ss);
                ExamResultChange.Pass = true;
                ExamResultChange.TotalPoint = 1;
                db.SaveChanges();
            }
            else
            {
                var ExamResultChange = db.Answers.Find(ss);
                ExamResultChange.Pass = false;
                ExamResultChange.TotalPoint = 1;
                db.SaveChanges();
            }
           
            
            return View(FindResult);

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Class.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }
        // GET: Classes/Create
        public ActionResult Create()
        {
            ViewBag.kutuphane = db.QuizLibrary.ToList();
            ViewBag.ogrenic = db.Students.ToList();
            return View();
        }
        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name, string QuizLib, string[] Student, string[] Library, Class @class)
        {
            if (ModelState.IsValid)
            {
                var Exist = db.Class.FirstOrDefault(x => x.Name == Name);
                if (Exist == null)
                {
                    @class.Name = Name;
                    @class.QuizLib = QuizLib;
                    @class.Students = string.Join("|", Student);
                    @class.QuizLib = string.Join("|", Library);
                    @class.CratedDate = DateTime.Now.ToString();
                    db.Class.Add(@class);
                    db.SaveChanges();
                    MailSender.MailGonder(@class.ID);
                    Logger.LogKeeper(Name + " Sınıfı oluşturdu ve mail gönderildi");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["exist"] = true;
                    return View(@class);
                }
            }
            return View();
        }
        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Class.Find(id);
            ViewBag.kutuphane = db.QuizLibrary.ToList();
            ViewBag.ogrenic = db.Students.ToList();
            if (!string.IsNullOrEmpty(@class.Students))
            {
                @class.Student = @class.Students.Split('|').ToArray();
            }
            if (!string.IsNullOrEmpty(@class.QuizLib))
            {
                @class.Library = @class.QuizLib.Split('|').ToArray();
            }
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }
        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Students,QuizLib,CratedDate")] Class @class, string[] Student, string[] Library)
        {
            if (ModelState.IsValid)
            {
                if (Student == null)
                {
                    @class.Students = null;
                }
                else
                {
                    @class.Students = string.Join("|", Student);
                }
                if (Library == null)
                {
                    @class.QuizLib = null;
                }
                else
                {
                    @class.QuizLib = string.Join("|", Library);
                }
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                MailSender.MailGonder(@class.ID);
                return RedirectToAction("Index");
            }
            return View(@class);
        }
        // GET: Classes/Delete/5
        // POST: Classes/Delete/5
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Class.Find(id);
            db.Class.Remove(@class);
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
