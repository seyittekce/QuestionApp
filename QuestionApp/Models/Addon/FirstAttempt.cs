using System.Linq;
namespace QuestionApp.Models.Addon
{
    public class FirstSett
    {
        private static DBContext db = new DBContext();
        public static void FirstAttempt()
        {
            if (db.Settings.Count() == 0)
            {
                Settings settings = new Settings();
                settings.First = 7;
                settings.Seccond = 10;
                settings.Host = "127.0.0.1";
                settings.Port = 587;
                settings.User_Name = "x@server.com";
                settings.User_password = "sifre";
                settings.site_adress = ""; //Request.Url.Scheme + "://" + Request.Url.Authority;
                settings.Layout = "horizontal";
                settings.Logo = "default.svg";
                settings.Sidebar_Background = "skin6";
                settings.Logo_Background = "skin6";
                settings.Navbar_Background = "skin6";
                settings.Preloader = false;
                db.Settings.Add(settings);
                db.SaveChanges();
            }
        }
    }
}