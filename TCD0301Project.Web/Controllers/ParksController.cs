using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TCD0301Project.Web.Models;
using TCD0301Project.Web.Utils;

namespace TCD0301Project.Web.Controllers
{
  public class ParksController : Controller
  {
    private IHttpClientFactory _clientFactory;
    public ParksController(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
      // Make the Api call
      var client = _clientFactory.CreateClient("ParkService");
      var response = await client.GetAsync(ServiceUrl.Park);

      // Process the response
      if (!response.IsSuccessStatusCode)
        return BadRequest("Something went wrong ...");

      // Convert JSON object to Model
      var parks = JsonConvert.DeserializeObject<List<ParkViewModel>>(
        response.Content.ReadAsStringAsync().Result);

      return View(parks);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      var client = _clientFactory.CreateClient("ParkService");
      var response = await client.GetAsync($"{ServiceUrl.Park}/{id}");

      if (!response.IsSuccessStatusCode)
        return BadRequest("Something went wrong ...");

      var park = JsonConvert.DeserializeObject<ParkViewModel>(
        response.Content.ReadAsStringAsync().Result);

      return View(park);
    }
  }
}
