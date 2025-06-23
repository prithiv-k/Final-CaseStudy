using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DAL.Models
{
    [Table("AuditLog")]
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }

        public int UserId { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        public DateTime Timestamp { get; set; }
    }
}