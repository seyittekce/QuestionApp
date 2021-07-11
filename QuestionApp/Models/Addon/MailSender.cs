using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
namespace QuestionApp.Models.Addon
{
    public class MailSender
    {
       
       
        public static void MailSend(string body,MailAddress toAddress)
        {
            DBContext db = new DBContext();
            Settings Mailer = db.Settings.FirstOrDefault();
            var fromAddress = new MailAddress(Mailer.Host);          
            const string subject = "Turan Bek | U.S.E WEB";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, Mailer.User_password)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
        public static void MailGonder(int SınıfID)
        {
            DBContext db = new DBContext();
            Class SınıfBul = db.Class.FirstOrDefault(x => x.ID == SınıfID);
            StringBuilder body = new StringBuilder();
            string[] Students = SınıfBul.Students.Split('|');
            foreach (var item in Students)
            {
              
                if (!string.IsNullOrEmpty(item))
                {
                    int ID = Convert.ToInt32(item);
                    var student = db.Students.FirstOrDefault(x=>x.ID==ID);
                    body.AppendLine("Merhaba!");
                    body.AppendLine(SınıfBul.StartDate.ToShortDateString()+" ile "+SınıfBul.EndDate.ToShortDateString()+" tarihleri arasında quiz oluşturulmuştur.");
                    body.AppendLine("Katılmanız Rica Olunur.");
                    var adres = new MailAddress(student.Mail);
                    MailSend(body.ToString(),adres);
                }
            }
        }
    }
}