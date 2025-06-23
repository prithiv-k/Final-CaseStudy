using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [Table("PayrollConfig")]
    public class PayrollConfig
    {
        [Key]
        public int ConfigId { get; set; }

        public int EmployeeId { get; set; }

        [Required]
        public decimal Allowances { get; set; }

        [Required]
        public decimal Deductions { get; set; }

        [Required]
        public decimal TaxRate { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}