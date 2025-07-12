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

    public EmployeeController(IEmployeeRepo<Employee> repo) // Dependency Injection for Employee Repository
    {
        _repo = repo;
    }

    [HttpGet("GetAll")] // Get all employees
    public IActionResult GetAll()
    {
        var result = _repo.GetAllEmployees();
        return Ok(result);
    }

    [HttpGet("{id}/GetById")] // Get employee by ID
    public IActionResult GetById(int id)
    {
        var result = _repo.GetEmployeeById(id);
        return Ok(result);
    }

    [HttpPost("Add")] // Add a new employee
    public IActionResult Add(Employee emp)
    {
        var result = _repo.AddEmployee(emp);
        return Ok(result);
    }

    [HttpPut("Update")] // Update an existing employee
    public IActionResult Update(Employee emp)
    {
        var result = _repo.UpdateEmployee(emp);
        return Ok(result);
    }

    [HttpDelete("Delete")] // Delete an employee
    public IActionResult Delete(Employee emp)
    {
        var result = _repo.DeleteEmployee(emp);
        return Ok(result);
    }
}