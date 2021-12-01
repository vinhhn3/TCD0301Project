using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using TCD0301Project.Backend.Data;
using TCD0301Project.Backend.Models;
using TCD0301Project.Backend.Repositories.Interfaces;

namespace TCD0301Project.Backend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ParksController : ControllerBase
  {
    private IParkRepository _parkRepos;
    public ParksController(IParkRepository parkRepos)
    {
      _parkRepos = parkRepos;
    }

    [HttpGet]
    public IActionResult GetParks()
    {
      var parks = _parkRepos.GetParks();
      return Ok(parks);
    }

    [HttpGet("{id:int}", Name = "GetPark")]
    public IActionResult GetPark(int id)
    {
      var park = _parkRepos.GetPark(id);
      if (park == null) return NotFound();

      return Ok(park);
    }

    [HttpPost]
    public IActionResult CreatePark([FromBody] Park park)
    {
      if (park == null) return BadRequest("The body is null ...");
      if (_parkRepos.ParkExists(park.Name))
      {
        return BadRequest("The name already existed ...");
      }

      if (!_parkRepos.CreatePark(park))
      {
        return BadRequest("Something went wrong ...");
      }

      return StatusCode(201);
    }

    [HttpPatch("{id:int}", Name = "UpdatePark")]
    public IActionResult UpdatePark(int id, [FromBody] Park park)
    {
      if (park == null || park.Id != id) return BadRequest("The body and the paramId don't match");

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
