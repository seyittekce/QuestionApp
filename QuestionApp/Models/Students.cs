using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace QuestionApp.Models
{
    public class Students
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Ad")]
        public string FirsName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Soyad")]

        public string LastName { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Kullanıcı Adı")]
        public string NickName { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail Adresi")]
        public string Mail { get; set; }
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Atandıdığı Sınıflar")]
        public string Clasess { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Oluşturulduğu Tarih")]
        public string CreatedDate { get; set; }
        public bool Verified { get; set; }
    }
}