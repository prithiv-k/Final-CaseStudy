using DAL.DataAccess;
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

    public NotificationController(INotificationRepo<Notification> repo) // Dependency Injection for Notification Repository
    {
        _repo = repo;
    }

    [HttpPost("Add")] // Adds a new notification
    public IActionResult Add(Notification notif)
    {
        var result = _repo.AddNotification(notif);
        return Ok(result);
    }

    [HttpGet("employee/{id}")] // Gets notifications by employee ID
    public IActionResult GetByEmployee(int id)
    {
        var result = _repo.GetNotificationsByEmployeeId(id);
        return Ok(result);
    }

    [HttpGet("GetAll")] // Gets all notifications
    public IActionResult GetAll()
    {
        var result = _repo.GetAll();
        return Ok(result);
    }
}