using System.ComponentModel;
namespace QuestionApp.Models
{
    public class Settings
    {
        public int ID { get; set; }
        [DisplayName("Host")]
        public string Host { get; set; }
        [DisplayName("Port")]
        public int Port { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string User_Name { get; set; }
        [DisplayName("Şifre")]
        public string User_password { get; set; }
        [DisplayName("İlk Uyarı Süresi")]
        public string site_adress { get; set; }
        [DisplayName("Menü Konumu")]
        public string Layout { get; set; }
        [DisplayName("Logo Rengi")]
        public string Logo_Background { get; set; }
        [DisplayName("Gezinti Çubuğu Rengi")]
        public string Navbar_Background { get; set; }
        [DisplayName("Yan Çubuk Rengi")]
        public string Sidebar_Background { get; set; }
        [DisplayName("Logo İmajı")]
        public string Logo { get; set; }
        [DisplayName("İlk Uyarı")]
        public int First { get; set; }
        [DisplayName("İkinci Uyarı")]
        public int Seccond { get; set; }
        [DisplayName("Ön Yükleyici(Pre-loader)")]
        public bool Preloader { get; set; }
    }
}