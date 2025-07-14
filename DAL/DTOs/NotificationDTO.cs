using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
    public class NotificationDTO
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(250)]
        public string Message { get; set; }

        public DateTime SentDate { get; set; } = DateTime.UtcNow;
    }
}
