using QuestionApp.Models;
using QuestionApp.Models.Addon;
using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web.Mvc;
namespace QuestionApp.Controllers
{
    public class StudentsController : Controller
    {
        private DBContext db = new DBContext();
        bool DahaonceGirdi(int QuestionID, int LibraryID)
        {
            var SoruMiktariHesapla = db.QuizLibrary.FirstOrDefault(x => x.ID == LibraryID);
            int SoruSayı = SoruMiktariHesapla.Questions.Split('|').ToArray().Length - 1;
            var OgrenciSon = db.Answers.Where(x => x.StudentID == StudentSide.ID && x.LibraryID == LibraryID).ToArray().LastOrDefault();
            if (OgrenciSon == null)
            {
                return true;
            }
            if (OgrenciSon.Status == "0")
            {
                return false;
            }
            if (OgrenciSon.QuestionID < SoruSayı)
            {
                OgrenciSon.Status = QuestionID.ToString() + "|";
                db.SaveChanges();
                return true;
            }
            else
            {
                OgrenciSon.Status = "0";
                int libid = SoruMiktariHesapla.ID;
                var Bul = db.Answers.Where(x => x.LibraryID == libid && x.StudentID == StudentSide.ID).ToList();
                int toplamsoru = 0;
                int dogrusoru = 0;
                int yanlissoru = 0;
                foreach (var item in Bul)
                {
                    if (item.Type != "Paragraph")
                    {
                        toplamsoru++;
                        if (item.Pass == true)
                        {
                            dogrusoru++;
                        }
                        else
                        {
                            yanlissoru++;
                        }
                    }
                }
                int hes = dogrusoru * 100;
                OgrenciSon.TotalPoint = hes / toplamsoru;
                db.SaveChanges();
                TempData["Girmis"] = "girmis";
                return false;
            }
        }
        [HttpGet]
        public ActionResult Result(int ID)
        {
            var FindLib = db.QuizLibrary.Find(ID);
            TempData["QuizID"] = FindLib.ID;
            var FindStudent = db.Students.Find(StudentSide.ID);
            var FindResult = db.Answers.Where(x => x.LibraryID == FindLib.ID && x.StudentID == FindStudent.ID).ToList();
            return View(FindResult);
        }
        public ActionResult Profile()
        {
            Students students = db.Students.Find(StudentSide.ID);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }
        [HttpPost]
        public ActionResult Profile(string eski_sifre, string yeni_sifre)
        {
            Students students = db.Students.Find(StudentSide.ID);
            try
            {
                var m = Regex.Match(yeni_sifre, @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,})");
                if (students.Password == MD5.MD5eDonustur(eski_sifre))
                {
                    if (m.Success == true && yeni_sifre != null)
                    {
                        students.Password = MD5.MD5eDonustur(yeni_sifre);
                        students.Verified = true;
                        db.SaveChanges();
                        Logger.LogKeeper("şifre değiştirdi");
                        TempData["sifre_degistirildi"] = "true";
                        return View(students);
                    }
                    else
                    {
                        TempData["sifre_degistirildi"] = "false";
                        return View(students);
                    }
                }
                else
                {
                    TempData["eski_sifre"] = "false";
                    return View(students);
                }
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                TempData["Error"] = "Hata";
                return View(students);
            }
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "ID,FirsName,LastName,NickName,Password,Mail,PhoneNumber,Clasess,TotalPoint,CreatedDate,Verified")] Students students)
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
                    students.Verified = false;
                    db.Students.Add(students);
                    Logger.LogKeeper(students.NickName + " adlı öğrenciyi kayıt oldu. Onay bekliyor");
                    db.SaveChanges();
                    TempData["Verified"] = false;
                    return RedirectToAction("StudentLogin", "Students");
                }
            }
            return View(students);
        }
        public ActionResult StudentLogOut()
        {
            try
            {
                StudentSide.LogOut();
                Session.RemoveAll();
                return RedirectToAction("StudentLogin", "Students");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentLogin(string NickName, string Password)
        {
            bool LoginOps = StudentSide.StLoginOps(NickName, Password);
            if (LoginOps == true)
            {
                Session["StudentLogin"] = true;
                if (StudentSide.Verfied() == false)
                {
                    TempData["Verfied"] = "first";
                    return View();
                }
                return RedirectToAction("StudentIndex");
            }
            else
            {
                TempData["hata"] = "hata";
            }
            return View();
        }
        string SendId = "";
        [HttpGet]
        public ActionResult StudentIndex()
        {
            var ClassFinder = db.Class.ToList();//tüm sınıfları çek
            ArrayList array = new ArrayList();//liste oluştur
            foreach (var item in ClassFinder)//tüm sınufları iteme ata
            {
                string[] StuFinder = item.Students.Split('|').ToArray();//sınıftaki tüm öğrencileri kümeye ata
                foreach (var Stu in StuFinder)//tüm sınıftaki öğrencileri gel 
                {
                    if (Stu != null)
                    {
                        if (Stu == StudentSide.ID.ToString())
                        {
                            array.Add(item.QuizLib);
                        }
                    }
                }
            }
            ViewBag.kutup = array.ToArray();
            return View();
        }
        [HttpPost]
        public JsonResult ModalResult(int ID)
        {
            Question question = db.Question.FirstOrDefault(o => o.ID == ID);
            return Json(question, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Exam(int LibraryID, int QuestionID)
        {
            if (StudentSide.ID <= 0)
            {
                //öğrenci giriş yapmış mı?
                return RedirectToAction("StudentLogin");
            }
            bool dahaonce = DahaonceGirdi(QuestionID, LibraryID);
            if (dahaonce == false)
            {
                return RedirectToAction("Result/" + LibraryID);
            }
            var ClassFinder = db.Class.ToList();
            //Öğrenci Sınıfta var mı ?
            foreach (var item in ClassFinder)
            {
                if (item != null)
                {
                    string[] Students = item.Students.Split('|').ToArray();
                    if (Students != null)
                    {
                        foreach (var Student in Students)
                        {
                            int stu = Convert.ToInt32(Student);
                            if (stu == StudentSide.ID)
                            {
                                var QuizLib = db.QuizLibrary.Where(x => x.ID == LibraryID).FirstOrDefault();
                                string[] Exam = QuizLib.Questions.Split('|').ToArray();
                                int Count = Exam.Count();
                                if (Count > QuestionID)
                                {
                                    int ExamID = Convert.ToInt32(Exam[QuestionID]);
                                    return View(db.Question.Find(ExamID));
                                }
                                else
                                {
                                    TempData["EndofExam"] = true;
                                    ViewBag.studentAnswer = db.Answers.Where(x => x.LibraryID == LibraryID && x.StudentID == StudentSide.ID).ToList();
                                    return View();
                                }
                            }
                        }
                    }
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Exam(int LibraryID, int QuestionID, Answers model, string Next, string selector)
        {
            if (StudentSide.ID <= 0)
            {
                //öğrenci giriş yapmış mı?
                return RedirectToAction("StudentLogin");
            }
            var ClassFinder = db.Class.ToList();
            //Öğrenci Sınıfta var mı ?
            foreach (var item in ClassFinder)
            {
                if (item != null)
                {
                    string[] Students = item.Students.Split('|').ToArray();
                    if (Students != null)
                    {
                        foreach (var Student in Students)
                        {
                            int stu = Convert.ToInt32(Student);
                            if (stu == StudentSide.ID)
                            {
                                if (Next != null)
                                {
                                    var QuizLib = db.QuizLibrary.Where(x => x.ID == LibraryID).FirstOrDefault();
                                    string[] Exam = QuizLib.Questions.Split('|').ToArray();
                                    int ExamID = Convert.ToInt32(Exam[QuestionID]);
                                    var QuestionFinder = db.Question.Find(ExamID);
                                    model.Pass = false;
                                    TempData["Answer"] = "False";
                                    if (QuestionFinder != null)
                                    {
                                        if (QuestionFinder.Type == "multiple")
                                        {
                                            if (selector == QuestionFinder.answers1)
                                            {
                                                model.Pass = true;
                                                TempData["Answer"] = "True";
                                            }
                                        }
                                        if (QuestionFinder.Type == "truefalse")
                                            if (selector == QuestionFinder.Answers)
                                            {
                                                model.Pass = true;
                                                TempData["Answer"] = "True";
                                            }
                                    }
                                    if (QuestionFinder.Type == "short")
                                    {
                                        string[] answers1 = selector.Split('|').ToArray();
                                        foreach (var ans in answers1)
                                        {
                                            if (!string.IsNullOrEmpty(ans))
                                            {
                                                if (selector == ans)
                                                {
                                                    model.Pass = true;
                                                    TempData["Answer"] = "True";
                                                }
                                            }
                                        }
                                    }
                                    TempData["Explain"] = QuestionFinder.Explanation;
                                    model.LibraryID = LibraryID;
                                    model.QuestionID = QuestionID;
                                    model.StudentID = StudentSide.ID;
                                    model.Answer = selector;
                                    model.Type = QuestionFinder.Type;
                                    model.ClassID = item.ID;
                                    db.Answers.Add(model);
                                    db.SaveChanges();
                                    QuestionID++;
                                    return RedirectToAction("Exam", new { LibraryID = LibraryID, QuestionID = QuestionID });
                                }
                            }
                        }
                    }
                }
            }
            Dispose();
            return View();
        }
        [HttpGet]
        public ActionResult ExamResult(int? ID)
        {
            return View();
        }
        public ActionResult Articles()
        {
            return View(db.Vt.ToList());
        }
        public ActionResult ReadArticles(int?ID)
        {
            return View(db.Vt.Find(ID));
        }
        public ActionResult Videos()
        {
            var ss = db.Vt.Where(x => x.Type == "Video").ToList();
            return View(ss);
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
