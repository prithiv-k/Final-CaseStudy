using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiVersion("4.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class PayrollController : ControllerBase
{
    private readonly IPayrollRepo<Payroll> _repo;

    public PayrollController(IPayrollRepo<Payroll> repo)
    {
        _repo = repo;
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("generate")]
    public IActionResult Generate(Payroll payroll)
    {
        var result = _repo.GeneratePayroll(payroll);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("Add")]
    public IActionResult Add(Payroll payroll)
    {
        var result = _repo.AddPayroll(payroll);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("Update")]
    public IActionResult Update(Payroll payroll)
    {
        var result = _repo.UpdatePayroll(payroll);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpDelete("Delete")]
    public IActionResult Delete(Payroll payroll)
    {
        var result = _repo.DeletePayroll(payroll);
        return Ok(result);
    }

    [Authorize(Roles = "Employee")]
    [HttpGet("employee/{id}")]
    public IActionResult GetByEmployee(int id)
    {
        var result = _repo.GetPayrollsByEmployeeId(id);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var result = _repo.GetAllPayrolls();
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("Verify/{id}")]
    public IActionResult Verify(int id)
    {
        var result = _repo.VerifyPayroll(id);
        return Ok(result);
    }
}
