using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ParkViewModel model)
    {
      if (!ModelState.IsValid)
        return View(model);

      var client = _clientFactory.CreateClient("ParkService");
      HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
        Encoding.UTF8, "application/json"
        );

      var response = await client.PostAsync(ServiceUrl.Park, content);

      if (!response.IsSuccessStatusCode) return BadRequest("Something went wrong ...");

      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      var client = _clientFactory.CreateClient("ParkService");

      var response = await client.DeleteAsync($"{ServiceUrl.Park}/{id}");

      if (!response.IsSuccessStatusCode) return BadRequest("Something went wrong ...");

      return RedirectToAction("Index");
    }
  }
}
