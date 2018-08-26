using Microsoft.AspNetCore.Mvc;

namespace Action.Services.Identity.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello From Identity API");
    }
}