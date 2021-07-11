using System;
using System.Linq;
namespace QuestionApp.Models.Addon
{
    public class Logger
    {
        private static DBContext db = new DBContext();
        private static void LogChecker()
        {
            var LogCount = db.Log.Count();
            if (LogCount >= 1000)
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Log]");
            }
        }
        public static void LogKeeper(string Action)
        {
            LogChecker();
            Log log = new Log();
            log.Action = LoginOps.NickName + " - " + Action;
            log.Date = DateTime.Now.ToString();
            db.Log.Add(log);
            db.SaveChanges();
        }
    }
}