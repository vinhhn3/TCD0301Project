using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using TCD0301Project.Backend.Data;
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
  }
}
