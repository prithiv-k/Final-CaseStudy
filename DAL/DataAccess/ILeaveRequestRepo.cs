using DAL.Models;

public interface ILeaveRequestRepo<T>
{
    T SubmitLeaveRequest(T request);
    T ApproveOrRejectLeaveRequest(T request);
    List<T> GetLeaveRequestsByEmployeeId(int employeeId);
    List<T> GetLeaveRequestsByEmail(string email); // ✅ NEW
    List<T> GetAllLeaveRequests();
    Employee GetEmployeeByEmail(string email);
}
