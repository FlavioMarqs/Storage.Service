using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Commands.Commands;
using Storage.Commands.Queries;
using Storage.Domain;
using Storage.Handlers.Interfaces;
using Storage.Service.Requests;
using System.Net;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly ICommandHandler<StringStoreCommand, StringDTO> _stringHandler;
        private readonly ICommandHandler<StringQueryCommand, StringDTO> _stringQueryHandler;
        private readonly IMapper _mapper;
        private readonly ILogger<StorageController> _logger;

        public StorageController(ILogger<StorageController> logger, ICommandHandler<StringStoreCommand, StringDTO> stringHandler, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _stringHandler = stringHandler ?? throw new ArgumentNullException(nameof(stringHandler));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPut(Name = "String")]
        public async Task<IActionResult> Put(StringCreationRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Value))
                return BadRequest("Invalid value submitted; cannot be null, empty or whitespaces.");

            _logger.LogInformation($"Storing string at {DateTime.UtcNow}.");
            
            try
            {
                var command = _mapper.Map<StringStoreCommand>(request);
                await _stringHandler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong when submitting a new StringStoreCommand", ex);
                return Conflict(ex.GetType());
            }

            return Ok();
        }

        [HttpGet(Name = "Strings")]
        public async Task<IActionResult> Get(StringQueryRequest request)
        {
            if (request.Identifier < 1)
                return BadRequest("Invalid value submitted; must be above zero.");

            _logger.LogInformation($"Querying for String with identifier {request.Identifier}");
            try
            {
                var command = _mapper.Map<StringQueryCommand>(request);
                await _stringQueryHandler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong when submitting a new StringStoreCommand", ex);
                return NotFound(request.Identifier);
            }
            return Ok();
        }
    }
}