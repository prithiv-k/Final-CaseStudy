using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin,Manager")]
[ApiVersion("1.0")]
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
    public IActionResult GetAll() => Ok(_repo.GetAllEmployees());
    [HttpGet("{id}/GetById")] 
    public IActionResult GetById(int id) => Ok(_repo.GetEmployeeById(id));
    [HttpPost("Add")] 
    public IActionResult Add(Employee emp) => Ok(_repo.AddEmployee(emp));
    [HttpPut("Update")]
    public IActionResult Update(Employee emp) => Ok(_repo.UpdateEmployee(emp));
    [HttpDelete("Delete")]
    public IActionResult Delete(Employee emp) => Ok(_repo.DeleteEmployee(emp));
}