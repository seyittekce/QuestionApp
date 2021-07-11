using System;
using System.Linq;
namespace QuestionApp.Models.Addon
{
    public class LoginOps
    {
        public static int ID { get; set; }
        public static string Name { get; set; }
        public static string LastName { get; set; }
        public static string PhoneNumber { get; set; }
        public static string Mail { get; set; }
        public static string NickName { get; set; }
        public static string AuthLevel { get; set; }
        public static bool First { get; set; }
        private static DBContext db = new DBContext();
        public static bool LoginUp(string UserName, string Password)
        {
            try
            {
                string Passwords = MD5.MD5eDonustur(Password);
                var Exist = db.Users.FirstOrDefault(x => x.NickName == UserName && x.Password == Passwords);
                if (Exist != null)
                {
                    ID = Exist.ID;
                    Name = Exist.Name;
                    LastName = Exist.LastName;
                    PhoneNumber = Exist.PhoneNumber;
                    Mail = Exist.Email;
                    AuthLevel = Exist.AuthLevel;
                    First = Exist.First;
                    NickName = Exist.NickName;
                    Logger.LogKeeper("Giriş Yaptı");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                return false;
            }
        }
        public static bool LoginDown()
        {
            try
            {
                ID = 0;
                Name = null;
                LastName = null;
                PhoneNumber = null;
                Mail = null;
                AuthLevel = null;
                Logger.LogKeeper("Çıkış Yaptı");
                return true;
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                return false;
            }
        }
        public static void FirstUser()
        {
            if (db.Users.Count() == 0)
            {
                Users users = new Users();
                users.Name = "Admin";
                users.LastName = "";
                users.NickName = "Admin";
                users.CreatedDate = DateTime.Now.ToString();
                users.Password = MD5.MD5eDonustur("admin");
                users.AuthLevel = "3";
                db.Users.Add(users);
                db.SaveChanges();
            }
        }
        public static bool isExist(string NickName)
        {
            var bul = db.Users.FirstOrDefault(x => x.NickName == NickName);
            if (bul == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}