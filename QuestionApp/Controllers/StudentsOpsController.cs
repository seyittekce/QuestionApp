using QuestionApp.Models;
using QuestionApp.Models.Addon;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
namespace QuestionApp.Controllers
{
    public class StudentsOpsController : Controller
    {
        private DBContext db = new DBContext();
        // GET: StudentsOps
        public ActionResult Index(string id)
        {
            if (id != null)
            {
                TempData["Non"] = "123";
                return View(db.Students.Where(x => x.Verified == false));

            }
            else
            {
                return View(db.Students.ToList());
            }


        }
        [HttpPost]
        public ActionResult Index(int ID)
        {
            if (ID != null)
            {
                var FindStudnt = db.Students.Find(ID);
                if (FindStudnt!=null)
                {
                    FindStudnt.Verified= true;
                    db.SaveChanges();
                }
                TempData["Non"] = "123";
                return View(db.Students.Where(x => x.Verified == false));
            }
            else
            {
                return View(db.Students.ToList());
            }


        }
        // GET: StudentsOps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }
        // GET: StudentsOps/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: StudentsOps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirsName,LastName,NickName,Password,Mail,PhoneNumber,Clasess,TotalPoint,CreatedDate,Verified")] Students students)
        {
            if (ModelState.IsValid)
            {
                var Exist = db.Students.FirstOrDefault(x => x.NickName == students.NickName);
                if (Exist != null)
                {
                    TempData["Exist"] = true;
                    return View();
                }
                else
                {
                    students.Password = MD5.MD5eDonustur(students.Password);
                    students.CreatedDate = DateTime.Now.ToString();
                    db.Students.Add(students);
                    Logger.LogKeeper(students.NickName + " adlı öğrenciyi ekledi");
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(students);
        }
        // GET: StudentsOps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }
        // POST: StudentsOps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirsName,LastName,NickName,Password,Mail,PhoneNumber,Clasess,TotalPoint,CreatedDate,Verified")] Students students)
        {
            if (ModelState.IsValid)
            {
                students.Password = MD5.MD5eDonustur(students.Password);
                students.Verified = false;
                Logger.LogKeeper(students.NickName + " adlı öğrenciyi güncelledi");
                db.Entry(students).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(students);
        }
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Students students = db.Students.Find(id);
            Logger.LogKeeper(students.NickName + " adlı öğreniciyi sildi. ");
            db.Students.Remove(students);
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
