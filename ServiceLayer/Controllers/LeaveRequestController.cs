using DAL.DataAccess;
using DAL.Models;
using DAL.DTOs; // ✅ Add this line to use LeaveRequestUpdateDTO
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

[Authorize(Roles = "Manager,Employee")]
[ApiVersion("7.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase
{
    private readonly ILeaveRequestRepo<LeaveRequest> _repo;

    public LeaveRequestController(ILeaveRequestRepo<LeaveRequest> repo)
    {
        _repo = repo;
    }

    [HttpPost("Submit")]
    public IActionResult Submit(LeaveRequestDTO req)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
            return Unauthorized("Email not found in token.");

        var employee = _repo.GetEmployeeByEmail(email); // 🔍 Must be implemented in your repo
        if (employee == null)
            return NotFound("Employee not found.");

        var leaveRequest = new LeaveRequest
        {
            EmployeeId = employee.EmployeeId,
            StartDate = req.StartDate,
            EndDate = req.EndDate,
            Status = req.Status
        };

        var result = _repo.SubmitLeaveRequest(leaveRequest);
        return Ok(result);
    }

    // ✅ Rewritten with DTO to avoid nested object issues
    [HttpPut("Approve")]
    public IActionResult ApproveOrReject([FromBody] LeaveRequestUpdateDTO req)
    {
        if (req == null || req.LeaveRequestId <= 0 || req.EmployeeId <= 0)
            return BadRequest("Invalid leave request data.");

        var leaveRequest = new LeaveRequest
        {
            LeaveRequestId = req.LeaveRequestId,
            EmployeeId = req.EmployeeId,
            StartDate = req.StartDate,
            EndDate = req.EndDate,
            Status = req.Status
        };

        var result = _repo.ApproveOrRejectLeaveRequest(leaveRequest);
        return Ok(result);
    }

    [HttpGet("employee/{id}")]
    public IActionResult GetByEmployee(int id)
    {
        var result = _repo.GetLeaveRequestsByEmployeeId(id);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var result = _repo.GetAllLeaveRequests();
        return Ok(result);
    }

    [HttpGet("my-leaves")]
    public IActionResult GetMyLeaveRequests()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
            return Unauthorized("Email claim not found in token.");

        var result = _repo.GetLeaveRequestsByEmail(email);
        return Ok(result);
    }
}
