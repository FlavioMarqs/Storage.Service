using Storage.Commands.Queries;
using Storage.DAOs;
using Storage.Handlers.Interfaces;
using Storage.Repositories.Interfaces;
using AutoMapper;
using Storage.Handlers.DTOs;

namespace Storage.Handlers
{
    public class StringQueryHandler : ICommandHandler<StringQueryCommand, StringDTO>
    {
        private readonly IStoreRepository<StringDAO> _repository;
        private readonly IMapper _mapper;
        public StringQueryHandler(IStoreRepository<StringDAO> repository, IMapper mapper) 
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<StringDTO> HandleAsync(StringQueryCommand command)
        {
            var queryResult = await _repository.GetById(command.Identifier);
            return _mapper.Map<StringDTO>(queryResult);
        }
    }
}
