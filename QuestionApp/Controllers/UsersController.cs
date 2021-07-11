using QuestionApp.Models;
using QuestionApp.Models.Addon;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
namespace QuestionApp.Controllers
{
    public class UsersController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Users
        public ActionResult Exit()
        {
            try
            {
                LoginOps.LoginDown();
                Session.RemoveAll();
                return RedirectToAction("Login", "Users");
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                return RedirectToAction("Login", "Users");
            }
        }
        public ActionResult Login()
        {
            FirstSett.FirstAttempt();
            LoginOps.FirstUser();
            if (LoginOps.ID != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult profile()
        {
            Users users = db.Users.Find(LoginOps.ID);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        [HttpPost]
        public ActionResult profile(string eski_sifre, string yeni_sifre)
        {
            try
            {
                Users users = db.Users.Find(LoginOps.ID);
                var m = Regex.Match(yeni_sifre, @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,})");
                if (users.Password == MD5.MD5eDonustur(eski_sifre))
                {
                    if (m.Success == true && yeni_sifre != null)
                    {
                        users.Password = MD5.MD5eDonustur(yeni_sifre);
                        users.First = true;
                        db.SaveChanges();
                        Logger.LogKeeper("şifre değiştirdi");
                        TempData["sifre_degistirildi"] = "true";
                        return View(users);
                    }
                    else
                    {
                        TempData["sifre_degistirildi"] = "false";
                        return View(users);
                    }
                }
                else
                {
                    TempData["eski_sifre"] = "false";
                    return View(users);
                }
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                TempData["Error"] = "Hata";
                return RedirectToAction("Login", "Users");
            }
        }
        [HttpPost]
        public ActionResult Login(string Username, string password)
        {
            try
            {
                bool LoginUp = LoginOps.LoginUp(Username, password);
                if (LoginUp == true)
                {
                    if (LoginOps.First == false)
                    {
                        TempData["First"] = true;
                        return RedirectToAction("profile", "Users");
                    }
                    Session["login"] = "true";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["LoginError"] = "Şifre veya kullanıcı adı yanlış";
                    return RedirectToAction("Login", "Users");
                }
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                TempData["Error"] = "Hata";
                return RedirectToAction("Login", "Users");
            }
        }
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,LastName,NickName,Password,Email,AuthLevel,PhoneNumber,CreatedDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                if (LoginOps.isExist(users.NickName) == true)
                {
                    string passswords = MD5.MD5eDonustur(users.Password);
                    users.Password = passswords;
                    users.CreatedDate = DateTime.Now.ToString();
                    db.Users.Add(users);
                    db.SaveChanges();
                    Logger.LogKeeper("kullanıcı ekledi.");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["exist"] = true;
                    return View();
                }
            }
            return View(users);
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LastName,NickName,Password,Email,AuthLevel,PhoneNumber,CreatedDate")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Users users = db.Users.Find(id);
                db.Users.Remove(users);
                db.SaveChanges();
                TempData["Message"] = "";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Logger.LogKeeper(e.ToString());
            }
            return View();
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
