using DAL.Models;
using Microsoft.EntityFrameworkCore;

public class LeaveRequestRepo : ILeaveRequestRepo<LeaveRequest>
{
    public LeaveRequest SubmitLeaveRequest(LeaveRequest request)
    {
        using (var dbContext = new EasypayContext())
        {
            dbContext.LeaveRequests.Add(request);
            dbContext.SaveChanges();
            return request;
        }
    }

    public LeaveRequest ApproveOrRejectLeaveRequest(LeaveRequest request)
    {
        using (var dbContext = new EasypayContext())
        {
            var existing = dbContext.LeaveRequests.FirstOrDefault(r => r.LeaveRequestId == request.LeaveRequestId);
            if (existing != null)
            {
                existing.Status = request.Status;
                dbContext.SaveChanges();
            }
            return existing;
        }
    }

    public List<LeaveRequest> GetLeaveRequestsByEmployeeId(int employeeId)
    {
        using (var dbContext = new EasypayContext())
        {
            return dbContext.LeaveRequests.Where(r => r.EmployeeId == employeeId).ToList();
        }
    }

    // ✅ NEW METHOD to fetch by email
    public List<LeaveRequest> GetLeaveRequestsByEmail(string email)
    {
        using (var dbContext = new EasypayContext())
        {
            return dbContext.LeaveRequests
                .Where(r => r.Employee.Email == email)
                .ToList();
        }
    }

    public List<LeaveRequest> GetAllLeaveRequests()
    {
        using (var dbContext = new EasypayContext())
        {
            return dbContext.LeaveRequests.ToList();
        }
    }
    public Employee GetEmployeeByEmail(string email)
    {
        using (var dbContext = new EasypayContext())
        {
            return dbContext.Employees.FirstOrDefault(e => e.Email == email);
        }
    }

}
