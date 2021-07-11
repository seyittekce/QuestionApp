using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace QuestionApp.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Sınıf Adı")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Öğrenciler")]
        public string Students { get; set; }
        public virtual Students Student { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Soru Kütüphanesi")]
        public string QuizLib { get; set; }
        public virtual Library Library { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("E-Mail Gönder")]
        public bool MailSend { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Bitiş Tarihi Tarihi")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CratedDate { get => CratedDate; set => Ekle(); }
        private DateTime Ekle()
        {
            return DateTime.Now;
        }
        public string[] Student1 { get; set; }
        public string[] Library1 { get; set; }
    }
}