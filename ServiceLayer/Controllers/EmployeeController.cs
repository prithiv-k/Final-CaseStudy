using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin,Manager,Employee")]
[ApiVersion("8.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepo<Employee> _repo;

    public EmployeeController(IEmployeeRepo<Employee> repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var result = _repo.GetAllEmployees();
        return Ok(result);
    }

    [HttpGet("{id}/GetById")]
    public IActionResult GetById(int id)
    {
        var result = _repo.GetEmployeeById(id);
        return Ok(result);
    }

    // ✅ Only keep this one GetByEmail endpoint
    [Authorize]
    [HttpGet("by-email/{email}")]
    public IActionResult GetByEmail(string email)
    {
        var result = _repo.GetEmployeeByEmail(email);
        if (result == null)
            return NotFound($"No employee found with email: {email}");

        return Ok(result);
    }

    [HttpPost("Add")]
    public IActionResult Add(Employee emp)
    {
        var result = _repo.AddEmployee(emp);
        return Ok(result);
    }

    [HttpPut("Update")]
    public IActionResult Update(Employee emp)
    {
        var result = _repo.UpdateEmployee(emp);
        return Ok(result);
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(Employee emp)
    {
        var result = _repo.DeleteEmployee(emp);
        return Ok(result);
    }
}
