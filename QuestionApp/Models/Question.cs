using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace QuestionApp.Models
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Soru Adı")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Soru")]
        public string Question { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Cevap")]
        public string Answers { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Açıklama")]
        public string Explanation { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Soru Tipi")]
        public string Type { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CratedDate { get; set; }
        private DateTime Ekle()
        {
            return DateTime.Now;
        }
        public string True_Answers { get; set; }

    }
}