namespace QuestionApp.Models
{
    public class LogError
    {
        public int ID { get; set; }
        public string Action { get; set; }
        public string Date { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorStatus { get; set; }
    }
}