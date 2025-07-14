using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.DTOs
{
    public class LeaveRequestDTO
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Pending, Approved, Rejected
    }
}
