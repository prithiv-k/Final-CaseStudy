using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
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

        public List<LeaveRequest> GetAllLeaveRequests()
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.LeaveRequests.ToList();
            }
        }
    }
}