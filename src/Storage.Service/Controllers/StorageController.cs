using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storage.Commands.Commands;
using Storage.Commands.Queries;
using Storage.DTOs.Requests;
using Storage.Handlers.DTOs;
using Storage.Handlers.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly ICommandHandler<StringStoreCommand, StringDTO> _stringHandler;
        private readonly ICommandHandler<StringQueryCommand, StringDTO> _stringQueryHandler;
        private readonly ICommandHandler<StringsQueryCommand, IEnumerable<StringDTO>> _stringsQueryHandler;
        private readonly IMapper _mapper;
        private readonly ILogger<StorageController> _logger;

        public StorageController(
            ILogger<StorageController> logger, 
            ICommandHandler<StringStoreCommand, StringDTO> stringHandler, 
            ICommandHandler<StringQueryCommand, StringDTO> stringQueryHandler, 
            ICommandHandler<StringsQueryCommand, IEnumerable<StringDTO>> stringsQueryHandler, 
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _stringHandler = stringHandler ?? throw new ArgumentNullException(nameof(stringHandler));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _stringQueryHandler = stringQueryHandler ?? throw new ArgumentNullException(nameof(stringQueryHandler));
            _stringsQueryHandler = stringsQueryHandler ?? throw new ArgumentNullException(nameof(stringsQueryHandler));
        }

        [HttpPost(Name = "Strings")]
        public async Task<IActionResult> Post(StringCreationRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Value))
                return BadRequest("Invalid value submitted; cannot be null, empty or whitespaces.");

            _logger.LogInformation($"Storing string at {DateTime.UtcNow}.");
            
            try
            {
                var command = _mapper.Map<StringStoreCommand>(request);
                return Ok(await _stringHandler.HandleAsync(command));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong when submitting a new StringStoreCommand", ex);
                return Conflict(ex.GetType());
            }

            return Ok();
        }

        [HttpGet(Name = "Strings/{identifier:int}")]
        public async Task<IActionResult> Get(int identifier)
        {
            if (identifier < 1)
                return BadRequest("Invalid value submitted; must be above zero.");

            _logger.LogInformation($"Querying for String with identifier {identifier}");
            try
            {
                var command = new StringQueryCommand { Identifier = identifier };
                return Ok(await _stringQueryHandler.HandleAsync(command));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong when submitting a new StringStoreCommand", ex);
                return NotFound(identifier);
            }
        }

        [HttpGet(Name = "Strings/all/{includeDeleted:bool}")]
        public async Task<IActionResult> Get(bool includeDeleted)
        {
            _logger.LogInformation($"Querying for Strings; includeDeleted={includeDeleted}");

            try
            {
                var command = new StringsQueryCommand { Identifier = includeDeleted };
                return Ok(await _stringsQueryHandler.HandleAsync(command));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong when submitting a new StringsQueryCommand", ex);
                return NotFound(ex.Message);
            }
        }
    }
}