using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("Payroll")]
    public class Payroll
    {
        [Key]
        public int PayrollId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        public decimal Salary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Bonus { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductions { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetSalary { get; set; }

        public DateTime PayrollDate { get; set; }
    }
}