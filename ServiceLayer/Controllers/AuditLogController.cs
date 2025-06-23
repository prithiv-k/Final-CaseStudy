using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuditLogController : ControllerBase
{
    private readonly IAuditLogRepo<AuditLog> _repo;

    public AuditLogController(IAuditLogRepo<AuditLog> repo)
    {
        _repo = repo;
    }

    [HttpPost("Add")]
    public IActionResult Add(AuditLog log) => Ok(_repo.AddLog(log));

    [HttpGet("GetAll")]
    public IActionResult GetAll() => Ok(_repo.GetAllLogs());
}