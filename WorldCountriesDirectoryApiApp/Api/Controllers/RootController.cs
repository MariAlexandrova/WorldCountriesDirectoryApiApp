using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static WorldCountriesDirectoryApiApp.Api.Messages.ApiMessages;

namespace WorldCountriesDirectoryApiApp.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public StringMessage Root()
        {
            return new StringMessage(Message: "server is running");
        }

        [HttpGet("ping")]
        public StringMessage Ping()
        {
            return new StringMessage(Message: "pong");
        }
    }
}
