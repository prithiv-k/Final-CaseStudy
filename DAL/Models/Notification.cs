using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        public int EmployeeId { get; set; }

        [Required, StringLength(250)]
        public string Message { get; set; }

        public DateTime SentDate { get; set; }

        public bool IsRead { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}