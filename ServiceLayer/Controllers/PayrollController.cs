using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class PayrollController : ControllerBase
{
    private readonly IPayrollRepo<Payroll> _repo;

    public PayrollController(IPayrollRepo<Payroll> repo)
    {
        _repo = repo;
    }

    [HttpPost("generate")] public IActionResult Generate(Payroll payroll) => Ok(_repo.GeneratePayroll(payroll));
    [HttpPost("Add")] public IActionResult Add(Payroll payroll) => Ok(_repo.AddPayroll(payroll));
    [HttpPut("Update")] public IActionResult Update(Payroll payroll) => Ok(_repo.UpdatePayroll(payroll));
    [HttpDelete("Delete")] public IActionResult Delete(Payroll payroll) => Ok(_repo.DeletePayroll(payroll));
    [HttpGet("employee/{id}")] public IActionResult GetByEmployee(int id) => Ok(_repo.GetPayrollsByEmployeeId(id));
    [HttpGet("GetAll")] public IActionResult GetAll() => Ok(_repo.GetAllPayrolls());
}