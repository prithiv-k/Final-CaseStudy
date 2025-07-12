using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiVersion("4.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class PayrollController : ControllerBase
{
    private readonly IPayrollRepo<Payroll> _repo;

    public PayrollController(IPayrollRepo<Payroll> repo) // Dependency Injection for Payroll Repository
    {
        _repo = repo;
    }

    [HttpPost("generate")] // Endpoint to generate payroll
    public IActionResult Generate(Payroll payroll)
    {
        var result = _repo.GeneratePayroll(payroll);
        return Ok(result);
    }

    [HttpPost("Add")] // Endpoint to add a new payroll record
    public IActionResult Add(Payroll payroll)
    {
        var result = _repo.AddPayroll(payroll);
        return Ok(result);
    }

    [HttpPut("Update")] // Endpoint to update an existing payroll record
    public IActionResult Update(Payroll payroll)
    {
        var result = _repo.UpdatePayroll(payroll);
        return Ok(result);
    }

    [HttpDelete("Delete")] // Endpoint to delete a payroll record
    public IActionResult Delete(Payroll payroll)
    {
        var result = _repo.DeletePayroll(payroll);
        return Ok(result);
    }

    [HttpGet("employee/{id}")] // Endpoint to get payroll by employee ID
    public IActionResult GetByEmployee(int id)
    {
        var result = _repo.GetPayrollsByEmployeeId(id);
        return Ok(result);
    }

    [HttpGet("GetAll")] // Endpoint to get all payroll records
    public IActionResult GetAll()
    {
        var result = _repo.GetAllPayrolls();
        return Ok(result);
    }

    [HttpGet("Verify/{id}")] // Endpoint to verify payroll by ID
    public IActionResult Verify(int id)
    {
        var result = _repo.VerifyPayroll(id);
        return Ok(result);
    }

}