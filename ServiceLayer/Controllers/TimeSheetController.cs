using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Employee")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class TimesheetController : ControllerBase
{
    private readonly ITimesheetRepo<Timesheet> _repo;

    public TimesheetController(ITimesheetRepo<Timesheet> repo) // Constructor injection for repository
    {
        _repo = repo;
    }

    [HttpPost("Add")] // Add or Update Timesheet
    public IActionResult AddOrUpdate([FromBody] Timesheet sheet)
    {
        var result = _repo.AddOrUpdateTimesheet(sheet);
        return Ok(result);
    }

    [HttpGet("employee/{id}/GetById")] // Get Timesheet by Employee ID
    public IActionResult GetByEmployee(int id)
    {
        var result = _repo.GetTimesheetsByEmployeeId(id);
        return Ok(result);
    }
}