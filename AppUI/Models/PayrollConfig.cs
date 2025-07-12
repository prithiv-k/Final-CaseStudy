namespace AppUI.Models
{
    public class PayrollConfig
    {
        public int ConfigId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public decimal TaxRate { get; set; }
        public Employee Employee { get; set; }
    }
}