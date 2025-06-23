using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin,Manager")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase
{
    private readonly ILeaveRequestRepo<LeaveRequest> _repo;

    public LeaveRequestController(ILeaveRequestRepo<LeaveRequest> repo)
    {
        _repo = repo;
    }

    [HttpPost("Submit")] public IActionResult Submit(LeaveRequest req) => Ok(_repo.SubmitLeaveRequest(req));
    [HttpPut("Approve or Reject")] public IActionResult ApproveOrReject(LeaveRequest req) => Ok(_repo.ApproveOrRejectLeaveRequest(req));
    [HttpGet("employee/{id}")] public IActionResult GetByEmployee(int id) => Ok(_repo.GetLeaveRequestsByEmployeeId(id));
    [HttpGet("GetAll")] public IActionResult GetAll() => Ok(_repo.GetAllLeaveRequests());
}