using DAL.DataAccess;
using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiVersion("10.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuditLogController : ControllerBase
{
    private readonly IAuditLogRepo<AuditLog> _repo;

    public AuditLogController(IAuditLogRepo<AuditLog> repo) // Constructor injection for the repository interface.
    {
        _repo = repo;
    }


    [HttpPost("Add")] // Adds a new Audit Log Entry.
    public IActionResult Add(AuditLog log)
    {
        var result = _repo.AddLog(log);
        return Ok(result);
    }

    [HttpGet("GetAll")] // Retrieves all Audit Log Entries.
    public IActionResult GetAll()
    {
        var result = _repo.GetAllLogs();
        return Ok(result);
    }
}