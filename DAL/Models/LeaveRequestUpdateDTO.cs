namespace DAL.DTOs
{
    public class LeaveRequestUpdateDTO
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
