using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TCD0301Project.Backend.Data;
using TCD0301Project.Backend.Dtos;
using TCD0301Project.Backend.Models;
using TCD0301Project.Backend.Repositories.Interfaces;

namespace TCD0301Project.Backend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ParksController : ControllerBase
  {
    private IParkRepository _parkRepos;
    private readonly IMapper _mapper;
    public ParksController(IParkRepository parkRepos, IMapper mapper)
    {
      _parkRepos = parkRepos;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetParks()
    {
      var parks = _parkRepos.GetParks();
      var parksDto = new List<ParkDto>();
      foreach (var park in parks)
      {
        parksDto.Add(_mapper.Map<ParkDto>(park));
      }
      return Ok(parksDto);
    }

    [HttpGet("{id:int}", Name = "GetPark")]
    public IActionResult GetPark(int id)
    {
      var park = _parkRepos.GetPark(id);
      if (park == null) return NotFound();

      //var parkDto = new ParkDto
      //{
      //  Established = park.Established,
      //  State = park.State,
      //  Name = park.Name
      //};
      var parkDto = _mapper.Map<ParkDto>(park);

      return Ok(parkDto);
    }

    [HttpPost]
    public IActionResult CreatePark([FromBody] ParkDto parkDto)
    {
      if (parkDto == null) return BadRequest("The body is null ...");
      if (_parkRepos.ParkExists(parkDto.Name))
      {
        return BadRequest("The name already existed ...");
      }

      var park = _mapper.Map<Park>(parkDto);

      if (!_parkRepos.CreatePark(park))
      {
        return BadRequest("Something went wrong ...");
      }

      return StatusCode(201);
    }

    [HttpPatch("{id:int}", Name = "UpdatePark")]
    public IActionResult UpdatePark(int id, [FromBody] ParkDto parkDto)
    {
      if (parkDto == null || parkDto.Id != id) return BadRequest("The body and the paramId don't match");

      var park = _mapper.Map<Park>(parkDto);

      if (!_parkRepos.UpdatePark(park))
      {
        return BadRequest("Something went wrong ...");
      }

      return Ok("Updated successfully ...");
    }

    [HttpDelete("{id:int}", Name = "DeletePark")]

    public IActionResult DeletePark(int id)
    {
      if (!_parkRepos.DeletePark(id))
      {
        return BadRequest("Something went wrong ...");
      }

      return Ok("Deleted Sucessfully ...");
    }
  }
}
