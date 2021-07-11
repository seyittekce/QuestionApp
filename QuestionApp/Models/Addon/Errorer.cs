using System;
using System.Linq;
namespace QuestionApp.Models.Addon
{
    public class Errorer
    {
        private static DBContext db = new DBContext();
        private static void ErrorChecker()
        {
            var LogCount = db.Error.Count();
            if (LogCount >= 1000)
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [LogErrors]");
            }
        }
        public static void ErrorKeeper(string Error)
        {
            ErrorChecker();
            LogError error = new LogError();
            error.Action = Error;
            error.Date = DateTime.Now.ToString();
            db.Error.Add(error);
            db.SaveChanges();
        }
    }
}