using Microsoft.AspNetCore.Mvc;

namespace Action.Services.Activities.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello From Activities API");
    }
}