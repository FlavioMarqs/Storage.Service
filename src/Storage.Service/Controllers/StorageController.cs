using Microsoft.AspNetCore.Mvc;
using Storage.Commands;
using Storage.Handlers.Interfaces;
using System.Net;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private ICommandHandler<StringStoreCommand> _stringHandler;

        private readonly ILogger<StorageController> _logger;

        public StorageController(ILogger<StorageController> logger, ICommandHandler<StringStoreCommand> stringHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _stringHandler = stringHandler ?? throw new ArgumentNullException(nameof(stringHandler));
        }

        [HttpPut(Name = "Strings")]
        public async Task<HttpStatusCode> Put(StringStoreCommand command)
        {
            if(string.IsNullOrWhiteSpace(command.Value))
                return HttpStatusCode.BadRequest;

            _logger.LogInformation($"Storing string at {DateTime.UtcNow}.");
            
            try
            {
                await _stringHandler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong when submitting a new StringStoreCommand", ex);
                return HttpStatusCode.Conflict;
            }

            return HttpStatusCode.OK;
        }
    }
}