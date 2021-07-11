using Microsoft.Ajax.Utilities;
using QuestionApp.Models;
using QuestionApp.Models.Addon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace QuestionApp.Controllers
{
    public class VTController : Controller
    {
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            return View(db.Vt.ToList());
        }
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideAndText vt = db.Vt.Find(id);
            if (vt == null)
            {
                return HttpNotFound();
            }
            return View(vt);
        }
        // GET: VT
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(string VeriGonder, VideAndText vt, string Baslik, string Gorsel)
        {
         
                vt.Yazilar = string.IsNullOrEmpty(VeriGonder) ? VeriGonder : VeriGonder;
                vt.Yazar = LoginOps.Name + " " + LoginOps.LastName;
                vt.dateTime = DateTime.Now;
            vt.Type = "Article";
                vt.Baslik = Baslik;
                db.Vt.Add(vt);
                db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult VideoCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult VideoCreate([Bind(Include = "ID,Type,Baslik,Link")] VideAndText vt)
        {
            vt.Type = "Video";
            vt.Yazar = LoginOps.Name + " " + LoginOps.LastName;
            vt.dateTime = DateTime.Now;
            db.Vt.Add(vt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideAndText @class = db.Vt.Find(id);
            db.Vt.Remove(@class);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}