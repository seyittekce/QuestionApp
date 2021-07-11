using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace QuestionApp.Models
{
    public class Users
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("İsim")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Soyisim")]
        public string LastName { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Kullanıcı Adı")]
        public string NickName { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail Adresi")]
        public string Email { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Yetki Seviyesi")]
        public string AuthLevel { get; set; }
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("Oluşturulduğu Tarih")]
        public string CreatedDate { get; set; }
        public bool First { get; set; }
    }
}