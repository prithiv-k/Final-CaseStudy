using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiVersion("9.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BenefitController : ControllerBase
{
    private readonly IBenefitRepo<Benefit> _repo;

    public BenefitController(IBenefitRepo<Benefit> repo) // Dependency Injection for Benefit Repository
    {
        _repo = repo;
    }

    [HttpGet("GetAll")] // Get all benefits
    public IActionResult GetAll()
    {
        var result = _repo.GetAllBenefits();
        return Ok(result);
    }

    [HttpPost("Add")] // Add a new benefit
    public IActionResult Add(Benefit benefit)
    {
        var result = _repo.AddBenefit(benefit);
        return Ok(result);
    }

    [HttpPut("Update")] // Update an existing benefit
    public IActionResult Update(Benefit benefit)
    {
        var result = _repo.UpdateBenefit(benefit);
        return Ok(result);
    }

    [HttpDelete("Delete")] // Delete a benefit
    public IActionResult Delete(Benefit benefit)
    {
        var result = _repo.DeleteBenefit(benefit);
        return Ok(result);
    }
}