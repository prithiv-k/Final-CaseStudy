namespace AppUI.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int EmployeeId { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
    }
}