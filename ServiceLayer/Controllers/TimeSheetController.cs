using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class TimesheetController : ControllerBase
{
    private readonly ITimesheetRepo<Timesheet> _repo;

    public TimesheetController(ITimesheetRepo<Timesheet> repo)
    {
        _repo = repo;
    }

    [HttpPost("Add")] public IActionResult AddOrUpdate(Timesheet sheet) => Ok(_repo.AddOrUpdateTimesheet(sheet));
    [HttpGet("employee/{id}/GetById")] public IActionResult GetByEmployee(int id) => Ok(_repo.GetTimesheetsByEmployeeId(id));
}
