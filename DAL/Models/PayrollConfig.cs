using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class PayrollConfig
    {
        [Key]
        public int ConfigId { get; set; }

        [Required(ErrorMessage = "Employee is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Allowances required")]
        public decimal Allowances { get; set; }

        [Required(ErrorMessage = "Deductions required")]
        public decimal Deductions { get; set; }

        [Required(ErrorMessage = "Tax rate required")]
        public decimal TaxRate { get; set; }

        [ForeignKey("EmployeeId")]
        [JsonIgnore] // ✅ Important to avoid JSON binding error
        public virtual Employee? Employee { get; set; } // ✅ Make this nullable
    }
}
