using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface ILeaveRequestRepo<T>
    {
        T SubmitLeaveRequest(T request);
        T ApproveOrRejectLeaveRequest(T request);
        List<T> GetLeaveRequestsByEmployeeId(int employeeId);
        List<T> GetAllLeaveRequests();
    }
}