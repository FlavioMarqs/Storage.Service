using AutoMapper;
using Storage.Commands.Commands;
using Storage.DAOs;
using Storage.DTOs.Responses;
using Storage.Handlers.DTOs;
using Storage.Handlers.Interfaces;
using Storage.Repositories.Interfaces;

namespace Storage.Handlers
{
    public class StringCommandHandler : ICommandHandler<StringStoreCommand, StringDTO>
    {
        private readonly IStoreRepository<StringDAO> _repository;
        private readonly IMapper _mapper;

        public StringCommandHandler(IStoreRepository<StringDAO> repository, IMapper mapper) 
        { 
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<StringDTO> HandleAsync(StringStoreCommand command)
        {
            var dao = _mapper.Map<StringDAO>(command);
            var result = await _repository.CreateAsync(dao);
            return _mapper.Map<StringDTO>(result);
        }
    }
}