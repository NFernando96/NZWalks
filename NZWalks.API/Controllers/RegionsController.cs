using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext _dbContext;

    public RegionsController(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var regionsDomain = _dbContext.Regions.ToList();

        var regionsDto = new List<RegionDto>();
        foreach (var regionDomain in regionsDomain)
        {
            regionsDto.Add(new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            });
        }

        return Ok(regionsDto);
    }


    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var regionDomain = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

        if (regionDomain == null)
        {
            return NotFound();
        }

        var regionDto = new RegionDto()
        {
            Id = regionDomain.Id,
            Name = regionDomain.Name,
            Code = regionDomain.Code,
            RegionImageUrl = regionDomain.RegionImageUrl,
        };
        return Ok(regionDto);
    }
}
