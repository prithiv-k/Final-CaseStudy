using DAL.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepo _reportRepo;

        public ReportController(IReportRepo reportRepo) // Dependency Injection for IReportRepo
        {
            _reportRepo = reportRepo;
        }

        [HttpGet("GetComplianceReports")] // Endpoint to get compliance reports
        public IActionResult GetComplianceReports()
        {
            var reports = _reportRepo.GetComplianceReports();
            return Ok(reports);
        }
    }
}