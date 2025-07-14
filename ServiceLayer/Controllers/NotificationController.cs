using DAL.DataAccess;
using DAL.DTOs;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin,Manager,Employee")]
[ApiVersion("6.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationRepo<Notification> _repo;

    public NotificationController(INotificationRepo<Notification> repo)
    {
        _repo = repo;
    }

    // ✅ POST: Add a new notification
    [HttpPost("Add")]
    public IActionResult Add([FromBody] NotificationDTO notif)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = new Notification
        {
            EmployeeId = notif.EmployeeId,
            Message = notif.Message,
            SentDate = notif.SentDate,
            IsRead = false
        };

        var result = _repo.AddNotification(entity);
        return Ok(result);
    }

    // ✅ GET: All notifications for a specific employee
    [HttpGet("employee/{id}")]
    public IActionResult GetByEmployee(int id)
    {
        var result = _repo.GetNotificationsByEmployeeId(id);
        return Ok(result);
    }

    // ✅ GET: All notifications (Admin/Manager use case)
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var result = _repo.GetAll();
        return Ok(result);
    }
}
