using QuestionApp.Models;
using QuestionApp.Models.Addon;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace QuestionApp.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        private DBContext db = new DBContext();
        public ActionResult MailSettings()
        {
            var Finder = db.Settings.FirstOrDefault();
            return View(Finder);
        }
        [HttpPost]
        public ActionResult MailSettings(string Host, int Port, string User_name, string User_password, int First, int Seccond)
        {
            var mailayari = db.Settings.FirstOrDefault();
            mailayari.Host = Host;
            mailayari.Port = Port;
            mailayari.User_Name = User_name;
            mailayari.User_password = User_password;
            mailayari.First = First;
            mailayari.Seccond = Seccond;
            mailayari.site_adress = Request.Url.Scheme + "://" + Request.Url.Authority;
            db.SaveChanges();
            Logger.LogKeeper("Mail Ayarı Değiştirdi");
            TempData["oldu"] = "true";
            return View(mailayari);
        }
        public void list()
        {
            wrappers wrappers = new wrappers();
            sidebar sidebar = new sidebar();
            IList<wrappers> wrappers1 = new List<wrappers>() {
                new wrappers(){ number="vertical", name="Dikey"},
                new wrappers(){ number="horizontal", name="Yatay"},
            };
            IList<sidebar> sidebars = new List<sidebar>() {
                new sidebar(){ number="skin1", name="Mavi"},
                new sidebar(){ number="skin2", name="Turuncu"},
                 new sidebar(){ number="skin3", name="Aqua"},
                new sidebar(){ number="skin4", name="Mor"},
                 new sidebar(){ number="skin5", name="Koyu"},
                new sidebar(){ number="skin6", name="Açık"},
            };
            ViewBag.topbar = wrappers1.ToList();
            ViewBag.sidebar = sidebars.ToList();
        }
        public ActionResult ThemeSettings()
        {
            list();
            return View(db.Settings.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult ThemeSettings(string Layout, string Navbar_Background, string Sidebar_Background, string Logo_Background, bool Preloader, HttpPostedFileBase Logo)
        {
            var temasettings = db.Settings.FirstOrDefault();
            if (Logo != null)
            {
                temasettings.Logo = Logo.FileName;
                var path = Path.Combine(Server.MapPath("~/Document"), temasettings.Logo);
                Logo.SaveAs(path);
            }
            list();
            temasettings.Layout = Layout;
            temasettings.Navbar_Background = Navbar_Background;
            temasettings.Sidebar_Background = Sidebar_Background;
            temasettings.Logo_Background = Logo_Background;
            temasettings.Preloader = Preloader;
            db.SaveChanges();
            Logger.LogKeeper("Tema Değiştirdi");
            TempData["oldu"] = "true";
            return View();
        }
    }
}