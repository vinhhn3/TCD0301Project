using Microsoft.AspNetCore.Mvc;

namespace TCD0301Project.Web.Controllers
{
  public class ParksController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
