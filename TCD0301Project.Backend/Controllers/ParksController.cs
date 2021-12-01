using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using TCD0301Project.Backend.Data;

namespace TCD0301Project.Backend.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ParksController : ControllerBase
  {
    private ApplicationDbContext _context;
    public ParksController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult GetParks()
    {
      var parks = _context.Parks.ToList();
      return Ok(parks);
    }
  }
}
