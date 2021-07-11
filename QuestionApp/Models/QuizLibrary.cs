using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace QuestionApp.Models
{
    public class QuizLibrary
    {
        [Key]
        public int LibraryID { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Kütüphane Adı")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Sorular")]
        public string Questions { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CratedDate { get => CratedDate; set => Ekle(); }
        private DateTime Ekle()
        {
            return DateTime.Now;
        }
        public string[] Question { get; set; }
        [DisplayName("Yorum")]
        public string Comment { get; set; }
    }
}