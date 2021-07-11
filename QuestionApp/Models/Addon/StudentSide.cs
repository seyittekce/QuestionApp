using System;
using System.Linq;
namespace QuestionApp.Models.Addon
{
    public class StudentSide
    {
        public static int ID { get; set; }
        public static string Name { get; set; }
        public static string LastName { get; set; }
        public static string PhoneNumber { get; set; }
        public static string Mail { get; set; }
        public static string NickNames { get; set; }
        public static bool Proved { get; set; }
        public static bool First { get; set; }
        public static string Password { get; set; }
        private static DBContext db = new DBContext();
        public static bool StLoginOps(string NickName, string Password)
        {
            try
            {
                string Encripted = MD5.MD5eDonustur(Password);
                var Finder = db.Students.FirstOrDefault(x => x.NickName == NickName && x.Password == Encripted);
                if (Finder != null)
                {
                    ID = Finder.ID;
                    Name = Finder.FirsName;
                    LastName = Finder.LastName;
                    NickNames = Finder.NickName;
                    PhoneNumber = Finder.PhoneNumber;
                    Password = Finder.Password;
                    Mail = Finder.Mail;
                    Proved = Finder.Verified;
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                return false;
            }
        }

        public static bool LogOut()
        {
            try
            {
                ID = 0;
                Name = null;
                LastName = null;
                NickNames = null;
                PhoneNumber = null;
                Mail = null;
                Password = null;
                Proved = false;
                return true;
            }
            catch (Exception e)
            {
                Errorer.ErrorKeeper(e.ToString());
                return false;
            }
        }
        public static bool Verfied()
        {
            if (ID != 0 || ID <= 0)
            {
                if (Proved == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}