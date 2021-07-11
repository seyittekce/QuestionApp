using QuestionApp.Models;
using QuestionApp.Models.Addon;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace QuestionApp.Controllers
{
    public class QuestionController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Question
        [HttpGet]
        [Compress]
        public ActionResult Index()
        {
            try
            {
                return View(db.Question.ToList());
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                TempData["Error"] = "error";
                return View();
            }
        }
        [Compress]
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                TempData["Error"] = "error";
                return View();
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Compress]
        public ActionResult Create(Question asking, string QuestionName, string[] fields, string answers, string multiple, string MultipleQuestion, string Multipleexp, string truefalse, string trueQuestion, string options, string trueexp, string shoranswers, string shortQuestion, string shortAnswer, string shortExplan, string File, string FileQuestion, HttpPostedFileBase FileUpload, string paragraph, string Parag)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (multiple != null)
                    {
                        string ekel = string.Join("|", fields);
                        asking.Name = QuestionName;
                        asking.answers1 = answers;
                        asking.Type = "multiple";
                        asking.Questions = MultipleQuestion;
                        asking.Explanation = Multipleexp;
                        asking.Answers = ekel;
                        db.Question.Add(asking);
                        db.SaveChanges();
                    }
                    if (truefalse != null)
                    {
                        asking.Type = "truefalse";
                        asking.Name = QuestionName;
                        asking.Questions = trueQuestion;
                        asking.Explanation = trueexp;
                        asking.Answers = options;
                        db.Question.Add(asking);
                        db.SaveChanges();
                    }
                    if (shoranswers != null)
                    {
                        asking.Type = "short";
                        asking.Name = QuestionName;
                        asking.Questions = shortQuestion;
                        asking.Explanation = shortExplan;
                        asking.Answers = shortAnswer;
                        db.Question.Add(asking);
                        db.SaveChanges();
                    }
                    if (File != null)
                    {
                        asking.Type = "File";
                        asking.Name = "";
                        asking.Questions = FileQuestion;
                        asking.Explanation = "";
                        string files = DateTime.Now.ToShortDateString() + FileUpload.FileName;
                        var path = Path.Combine(Server.MapPath("~/Document"), files);
                        FileUpload.SaveAs(path);
                        asking.Answers = files;
                        db.Question.Add(asking);
                        db.SaveChanges();
                    }
                    if (Parag != null)
                    {
                        asking.Type = "Paragraph";
                        asking.Name = "";
                        asking.Questions = paragraph;
                        asking.Explanation = "";
                        asking.Answers = "";
                        db.Question.Add(asking);
                        db.SaveChanges();
                    }
                    Logger.LogKeeper("Soru Ekledi");
                    return Redirect("/Question/details/" + asking.ID);
                }
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                TempData["Error"] = "error";
                return View(asking);
            }
            return View(asking);
        }
        [Compress]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question asking = db.Question.Find(id);
            if (asking.Type == "multiple")
            {
                string[] da = asking.Answers.Split('|').ToArray();
                ViewBag.da = da.ToList();
            }
            if (asking.Type == "short")
            {
                string[] shotrs = asking.Answers.Split(',').ToArray();
                ViewBag.shorta = shotrs.ToList();
            }
            if (asking == null)
            {
                return HttpNotFound();
            }
            return View(asking);
        }
        [Compress]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Question asking = db.Question.Find(id);
                if (asking == null)
                {
                    return HttpNotFound();
                }
                return View(asking);
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                TempData["Error"] = "error";
                return View(db.Question.Find(id));
            }

        }
        // POST: Askings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Compress]
        public ActionResult Edit([Bind(Include = "ID,Type,Question,Answers,Explanation")] Question asking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(asking).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(asking);
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                TempData["Error"] = "error";
                return RedirectToAction("Index");
            }

        }
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        [Compress]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Question asking = db.Question.Find(id);
                db.Question.Remove(asking);
                db.SaveChanges();
                TempData["MessageMessage"] = "error";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                TempData["Error"] = "error";
                return RedirectToAction("Index");
            }
        }
    }
}