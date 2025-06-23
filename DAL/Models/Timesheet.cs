using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("Timesheet")]
    public class Timesheet
    {
        [Key]
        public int TimesheetId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public double HoursWorked { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}