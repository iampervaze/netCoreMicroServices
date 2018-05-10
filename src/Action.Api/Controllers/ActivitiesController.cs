using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using Action.Common.Commands;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Action.Api.Controllers
{
    [Route("activities")]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient _busClient;
        public ActivitiesController(IBusClient busClient) {
            _busClient = busClient;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateActivity command) {
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);
            return Accepted($"activities/{command.Id}");
        }


        [HttpGet("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get() => Content("secured");
    }
}