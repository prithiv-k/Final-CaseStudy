using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BenefitController : ControllerBase
{
    private readonly IBenefitRepo<Benefit> _repo;

    public BenefitController(IBenefitRepo<Benefit> repo)
    {
        _repo = repo;
    }

    [HttpGet("GetAll")] public IActionResult GetAll() => Ok(_repo.GetAllBenefits());
    [HttpPost("Add")] public IActionResult Add(Benefit benefit) => Ok(_repo.AddBenefit(benefit));
    [HttpPut("Update")] public IActionResult Update(Benefit benefit) => Ok(_repo.UpdateBenefit(benefit));
    [HttpDelete("Delete")] public IActionResult Delete(Benefit benefit) => Ok(_repo.DeleteBenefit(benefit));
}