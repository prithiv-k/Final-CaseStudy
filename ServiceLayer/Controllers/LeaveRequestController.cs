using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Manager,Employee")]
[ApiVersion("7.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase
{
    private readonly ILeaveRequestRepo<LeaveRequest> _repo;

    public LeaveRequestController(ILeaveRequestRepo<LeaveRequest> repo) // Dependency Injection for the repository
    {
        _repo = repo;
    }

    [HttpPost("Submit")] // Endpoint to submit a leave request
    public IActionResult Submit(LeaveRequest req)
    {
        var result = _repo.SubmitLeaveRequest(req);
        return Ok(result);
    }

    [HttpPut("Approve")] // Endpoint to approve or reject a leave request
    public IActionResult ApproveOrReject(LeaveRequest req)
    {
        var result = _repo.ApproveOrRejectLeaveRequest(req);
        return Ok(result);
    }

    [HttpGet("employee/{id}")] // Endpoint to get leave requests by employee ID
    public IActionResult GetByEmployee(int id)
    {
        var result = _repo.GetLeaveRequestsByEmployeeId(id);
        return Ok(result);
    }

    [HttpGet("GetAll")] // Endpoint to get all leave requests
    public IActionResult GetAll()
    {
        var result = _repo.GetAllLeaveRequests();
        return Ok(result);
    }
}