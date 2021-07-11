using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace QuestionApp.Models
{
    public class Answers
    {

        [Key]
        public int AnswerID { get; set; }
        [DisplayName("Öğrenci Numarası")]
        public int StudentID { get; set; }
        public virtual Students Students { get; set; }
        public int QuestionID { get; set; }
        public virtual Question Questions { get; set; }
        public int LibraryID { get; set; }
        public virtual QuizLibrary Library { get; set; }
        public int ClassID { get; set; }
        public virtual Class Class { get; set; }
        public string Essiz { get; set; }
        public string Answer { get; set; }
        public bool Pass { get; set; }
        public string Status { get; set; }
        public int TotalPoint { get; set; }
        public string Type { get; set; }
        public DateTime CratedDate { get; set; }
      

    }
}