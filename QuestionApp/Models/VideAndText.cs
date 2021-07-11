using QuestionApp.Models.Addon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuestionApp.Models
{
    public class VideAndText
    {
        public int ID { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Başlık")]
        public string Baslik { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Makaleler")]
        public string Yazilar { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("Oluşturulan Zaman")]

        public DateTime dateTime { get; set; }
        [DataType(DataType.Text)]
        [DisplayName("Yazar")]
        public string Yazar { get; set; }

        
        [DisplayName("Video Linki (Sadece Youtube)")]
        public string Link { get; set; }
        public string Type { get; set; }


    }
}