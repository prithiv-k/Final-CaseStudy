using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DAL.Models
{
    [Table("Benefit")]
    public class Benefit
    {
        [Key]
        public int BenefitId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}