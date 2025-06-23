using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationRepo<Notification> _repo;

    public NotificationController(INotificationRepo<Notification> repo)
    {
        _repo = repo;
    }

    [HttpPost("Add")] public IActionResult Add(Notification notif) => Ok(_repo.AddNotification(notif));
    [HttpGet("employee/{id}")] public IActionResult GetByEmployee(int id) => Ok(_repo.GetNotificationsByEmployeeId(id));
}