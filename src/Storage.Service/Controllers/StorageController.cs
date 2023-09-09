using Microsoft.AspNetCore.Mvc;
using Storage.Commands;
using System.Net;
using System.Windows.Input;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<StorageController> _logger;

        public StorageController(ILogger<StorageController> logger)
        {
            _logger = logger;
        }

        [HttpPut(Name = "Store")]
        public async HttpStatusCode Put(StoreCommand<string> command)
        {
            _logger.LogInformation($"Storing string at {DateTime.UtcNow}.");
            await _stringHandler.Handle(command);
            return HttpStatusCode.OK;
        }
    }
}