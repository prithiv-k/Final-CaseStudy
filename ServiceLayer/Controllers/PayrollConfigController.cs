using DAL.Models;
using DAL.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiVersion("5.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PayrollConfigController : ControllerBase
    {
        private readonly IPayrollConfigRepo<PayrollConfig> _repo;

        public PayrollConfigController(IPayrollConfigRepo<PayrollConfig> repo)
        {
            _repo = repo;
        }

        [HttpPost("AddorUpdate")]
        public IActionResult AddOrUpdate([FromBody] PayrollConfig config)
        {
            config.Employee = null; // ✅ prevent EF model binding issue

            if (!TryValidateModel(config))
            {
                return BadRequest(ModelState);
            }

            var result = _repo.AddOrUpdateConfig(config);
            return Ok(result);
        }




        [HttpGet("GetById/{employeeId}")]
        public IActionResult GetByEmployee(int employeeId)
        {
            var result = _repo.GetConfigByEmployeeId(employeeId);
            if (result == null)
                return NotFound("No payroll config found for this employee.");

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _repo.GetAll();
            return Ok(result);
        }
    }
}
