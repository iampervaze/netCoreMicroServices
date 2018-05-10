using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Action.Services.Identity.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]AuthenticateUser command)
        {
            return Json(await _userService.LoginAsync(command.Email, command.Password));
        }
    }
}